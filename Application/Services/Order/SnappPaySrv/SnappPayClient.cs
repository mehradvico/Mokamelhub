using Application.Services.Order.SnappPaySrv.Dto;
using Application.Services.Order.SnappPaySrv.Iface;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.Order.SnappPaySrv
{
    public class SnappPayClient : ISnappPayClient
    {
        private static readonly SemaphoreSlim TokenLock = new(1, 1);
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;
        private readonly SnappPayOptions _options;
        private readonly ILogger<SnappPayClient> _logger;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        public SnappPayClient(
            IHttpClientFactory httpClientFactory,
            IMemoryCache cache,
            IOptions<SnappPayOptions> options,
            ILogger<SnappPayClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
            _options = options.Value;
            _logger = logger;
        }

        public Task<SnappPayCallResult<SnappPayEligibilityResponse>> GetEligibilityAsync(
            long amountRial,
            IEnumerable<string> paymentMethodTypes = null)
        {
            var path = $"api/online/offer/v1/eligible?amount={amountRial}";
            var methods = paymentMethodTypes?.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            if (methods?.Length > 0)
            {
                path += $"&paymentMethodTypes={Uri.EscapeDataString(string.Join(',', methods))}";
            }

            return SendAsync<SnappPayEligibilityResponse>(HttpMethod.Get, path);
        }

        public Task<SnappPayCallResult<SnappPayPaymentTokenResponse>> CreatePaymentTokenAsync(SnappPayPaymentTokenRequest request) =>
            SendAsync<SnappPayPaymentTokenResponse>(HttpMethod.Post, "api/online/payment/v1/token", request);

        public Task<SnappPayCallResult<SnappPayTransactionResponse>> VerifyAsync(string paymentToken) =>
            SendAsync<SnappPayTransactionResponse>(HttpMethod.Post, "api/online/payment/v1/verify", new SnappPayTokenRequest { PaymentToken = paymentToken });

        public Task<SnappPayCallResult<SnappPayTransactionResponse>> SettleAsync(string paymentToken) =>
            SendAsync<SnappPayTransactionResponse>(HttpMethod.Post, "api/online/payment/v1/settle", new SnappPayTokenRequest { PaymentToken = paymentToken });

        public Task<SnappPayCallResult<SnappPayPaymentStatusResponse>> GetPaymentStatusAsync(string paymentToken) =>
            SendAsync<SnappPayPaymentStatusResponse>(HttpMethod.Get, $"api/online/payment/v1/status?paymentToken={Uri.EscapeDataString(paymentToken ?? string.Empty)}");

        public Task<SnappPayCallResult<SnappPayTransactionResponse>> UpdateAsync(SnappPayUpdateRequest request) =>
            SendAsync<SnappPayTransactionResponse>(HttpMethod.Post, "api/online/payment/v1/update", request);

        public Task<SnappPayCallResult<SnappPayTransactionResponse>> CancelAsync(string paymentToken) =>
            SendAsync<SnappPayTransactionResponse>(HttpMethod.Post, "api/online/payment/v1/cancel", new SnappPayTokenRequest { PaymentToken = paymentToken });

        private async Task<SnappPayCallResult<T>> SendAsync<T>(HttpMethod method, string path, object body = null)
        {
            var configError = ValidateConfiguration();
            if (configError != null)
            {
                return SnappPayCallResult<T>.Failure(configError);
            }

            try
            {
                var result = await SendOnceAsync<T>(method, path, body, false);
                if (result.HttpStatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    RemoveCachedToken();
                    result = await SendOnceAsync<T>(method, path, body, true);
                }

                return result;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning(ex, "SnappPay request timed out for {Path}", path);
                return SnappPayCallResult<T>.Failure("مهلت دریافت پاسخ از اسنپ‌پی تمام شد.", isTimeout: true);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "SnappPay transport error for {Path}", path);
                return SnappPayCallResult<T>.Failure("ارتباط با سرویس اسنپ‌پی برقرار نشد.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected SnappPay error for {Path}", path);
                return SnappPayCallResult<T>.Failure("خطای پیش‌بینی‌نشده در ارتباط با اسنپ‌پی رخ داد.");
            }
        }

        private async Task<SnappPayCallResult<T>> SendOnceAsync<T>(HttpMethod method, string path, object body, bool forceTokenRefresh)
        {
            var tokenResult = await GetAccessTokenAsync(forceTokenRefresh);
            if (!tokenResult.IsSuccess)
            {
                return SnappPayCallResult<T>.Failure(tokenResult.ErrorMessage, tokenResult.HttpStatusCode, tokenResult.ErrorCode);
            }

            using var request = new HttpRequestMessage(method, BuildUri(path));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResult.Data);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (body != null)
            {
                request.Content = new StringContent(JsonSerializer.Serialize(body, _jsonOptions), Encoding.UTF8, "application/json");
            }

            using var client = CreateClient();
            using var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            SnappPayApiResponse<T> envelope = null;
            if (!string.IsNullOrWhiteSpace(content))
            {
                try
                {
                    envelope = JsonSerializer.Deserialize<SnappPayApiResponse<T>>(content, _jsonOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogWarning(ex, "Invalid SnappPay JSON response for {Path}", path);
                }
            }

            if (response.IsSuccessStatusCode && envelope?.Successful == true)
            {
                return SnappPayCallResult<T>.Success(envelope.Response, (int)response.StatusCode);
            }

            var message = envelope?.ErrorData?.Message;
            if (string.IsNullOrWhiteSpace(message))
            {
                message = $"اسنپ‌پی پاسخ ناموفق با کد {(int)response.StatusCode} برگرداند.";
            }

            _logger.LogWarning(
                "SnappPay rejected {Path}. HttpStatus: {HttpStatus}, ErrorCode: {ErrorCode}, Message: {Message}",
                path,
                (int)response.StatusCode,
                envelope?.ErrorData?.ErrorCode,
                message);

            return SnappPayCallResult<T>.Failure(message, (int)response.StatusCode, envelope?.ErrorData?.ErrorCode);
        }

        private async Task<SnappPayCallResult<string>> GetAccessTokenAsync(bool forceRefresh)
        {
            var cacheKey = GetTokenCacheKey();
            if (!forceRefresh && _cache.TryGetValue<string>(cacheKey, out var cachedToken) && !string.IsNullOrWhiteSpace(cachedToken))
            {
                return SnappPayCallResult<string>.Success(cachedToken, 200);
            }

            await TokenLock.WaitAsync();
            try
            {
                if (!forceRefresh && _cache.TryGetValue<string>(cacheKey, out cachedToken) && !string.IsNullOrWhiteSpace(cachedToken))
                {
                    return SnappPayCallResult<string>.Success(cachedToken, 200);
                }

                using var request = new HttpRequestMessage(HttpMethod.Post, BuildUri("api/online/v1/oauth/token"));
                var basicValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_options.ClientId}:{_options.ClientSecret}"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", basicValue);
                request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "password",
                    ["scope"] = "online-merchant",
                    ["username"] = _options.UserName,
                    ["password"] = _options.Password
                });

                using var client = CreateClient();
                using var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                SnappPayOAuthResponse oauth = null;
                try
                {
                    oauth = JsonSerializer.Deserialize<SnappPayOAuthResponse>(content, _jsonOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogWarning(ex, "Invalid SnappPay OAuth response.");
                }

                if (!response.IsSuccessStatusCode || string.IsNullOrWhiteSpace(oauth?.AccessToken))
                {
                    var message = response.StatusCode == HttpStatusCode.Forbidden
                        ? "دسترسی اسنپ‌پی رد شد؛ IP سرور هنوز Whitelist نشده است."
                        : "احراز هویت اسنپ‌پی ناموفق بود.";
                    return SnappPayCallResult<string>.Failure(message, (int)response.StatusCode);
                }

                var cacheSeconds = Math.Max(60, oauth.ExpiresIn - 60);
                _cache.Set(cacheKey, oauth.AccessToken, TimeSpan.FromSeconds(cacheSeconds));
                return SnappPayCallResult<string>.Success(oauth.AccessToken, (int)response.StatusCode);
            }
            finally
            {
                TokenLock.Release();
            }
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient("SnappPay");
            client.Timeout = TimeSpan.FromSeconds(Math.Clamp(_options.TimeoutSeconds, 5, 120));
            return client;
        }

        private Uri BuildUri(string path)
        {
            var baseUrl = (_options.BaseUrl ?? string.Empty).TrimEnd('/') + "/";
            return new Uri(new Uri(baseUrl, UriKind.Absolute), path.TrimStart('/'));
        }

        private string ValidateConfiguration()
        {
            if (!_options.Enabled)
                return "درگاه اسنپ‌پی در تنظیمات بک‌اند فعال نشده است.";

            if (!Uri.TryCreate(_options.BaseUrl, UriKind.Absolute, out _))
                return "آدرس سرویس اسنپ‌پی معتبر نیست.";

            if (string.IsNullOrWhiteSpace(_options.ClientId) ||
                string.IsNullOrWhiteSpace(_options.ClientSecret) ||
                string.IsNullOrWhiteSpace(_options.UserName) ||
                string.IsNullOrWhiteSpace(_options.Password))
                return "اطلاعات دسترسی اسنپ‌پی روی سرور تنظیم نشده است.";

            return null;
        }

        private string GetTokenCacheKey() => $"snapppay-token:{_options.BaseUrl}:{_options.ClientId}";

        private void RemoveCachedToken() => _cache.Remove(GetTokenCacheKey());
    }
}
