using Application.Common.Enumerable;
using Application.Common.Helpers.Iface;
using Application.Services.Order.PaymentGatewaySrv.Dto;
using Application.Services.Order.PaymentGatewaySrv.Iface;
using Application.Services.Order.ProductOrderSrv.Dto;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services.Order.PaymentGatewaySrv.Gateways
{
    public class ZarinPalGateway : IPaymentGateway
    {
        public MerchantEnum Provider => MerchantEnum.zarinpal;

        private readonly HttpClient _httpClient;

        private const string ZarinPalApiUrl = "https://payment.zarinpal.com/pg/v4/payment/request.json";
        private const string ZarinPalVerificationUrl = "https://payment.zarinpal.com/pg/v4/payment/verify.json";
        private const string ZarinPalStartPayUrl = "https://payment.zarinpal.com/pg/StartPay/";
        private const string AllowedCallbackHost = "payment.mokamelhub.com";
        private const string AllowedCallbackPathPrefix = "/callback/";

        public ZarinPalGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GatewayStartResultDto> StartAsync(PaymentStartDto dto, Merchant merchant)
        {
            try
            {
                if (merchant == null || string.IsNullOrWhiteSpace(merchant.MerchantNo))
                {
                    return new GatewayStartResultDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "مرچنت کد زرین‌پال ثبت نشده است."
                    };
                }

                if (dto.Amount <= 0)
                {
                    return new GatewayStartResultDto
                    {
                        IsSuccess = false,
                        ErrorMessage = Resource.Notification.InvalidData
                    };
                }

                var callbackUrl = dto.CallbackUrl;

                if (!IsValidCallbackUrl(callbackUrl, dto.PaymentId))
                {
                    return new GatewayStartResultDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "آدرس callback پرداخت معتبر نیست."
                    };
                }

                var requestData = new
                {
                    merchant_id = merchant.MerchantNo,
                    amount = Convert.ToInt64(dto.Amount),
                    currency = "IRT",
                    callback_url = callbackUrl,
                    description = string.Format(Resource.Pattern.PaymentDescription, dto.ProductOrderId),
                    metadata = new
                    {
                        email = dto.User?.Email,
                        mobile = dto.User?.Mobile
                    }
                };

                var content = new StringContent(
                    JsonSerializer.Serialize(requestData),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync(ZarinPalApiUrl, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new GatewayStartResultDto
                    {
                        IsSuccess = false,
                        ErrorMessage = $"خطا در ارتباط با زرین‌پال: {response.StatusCode} - {result}"
                    };
                }

                var jsonResponse = JsonSerializer.Deserialize<ZarinPalPaymentResponse>(
                    result,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (jsonResponse?.data == null)
                {
                    return new GatewayStartResultDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "پاسخ زرین‌پال نامعتبر است."
                    };
                }

                if (jsonResponse.data.code != 100 || string.IsNullOrWhiteSpace(jsonResponse.data.authority))
                {
                    return new GatewayStartResultDto
                    {
                        IsSuccess = false,
                        ErrorMessage = $"خطا در ایجاد تراکنش زرین‌پال. Code: {jsonResponse.data.code}"
                    };
                }

                return new GatewayStartResultDto
                {
                    IsSuccess = true,
                    PaymentIsLink = true,
                    PaymentUrl = $"{ZarinPalStartPayUrl}{jsonResponse.data.authority}",
                    Token = jsonResponse.data.authority
                };
            }
            catch (Exception ex)
            {
                return new GatewayStartResultDto
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<GatewayCallbackResultDto> CallbackAsync(Payment payment, Merchant merchant, HttpRequest request)
        {
            try
            {
                if (payment == null || merchant == null || string.IsNullOrWhiteSpace(merchant.MerchantNo))
                {
                    return new GatewayCallbackResultDto
                    {
                        IsSuccess = false,
                        ErrorMessage = Resource.Notification.InvalidData
                    };
                }

                var status = request.Query["Status"].ToString();
                var authority = request.Query["Authority"].ToString();

                if (string.IsNullOrWhiteSpace(status) || string.IsNullOrWhiteSpace(authority))
                {
                    return new GatewayCallbackResultDto
                    {
                        IsSuccess = false,
                        ErrorMessage = Resource.Notification.InvalidData
                    };
                }

                if (!string.Equals(status.Trim(), "OK", StringComparison.OrdinalIgnoreCase))
                {
                    return new GatewayCallbackResultDto
                    {
                        IsSuccess = false,
                        Token = authority,
                        ErrorMessage = Resource.Notification.Unsuccess,
                        Description = "پرداخت توسط کاربر لغو شد یا ناموفق بود."
                    };
                }

                var verifyData = new
                {
                    merchant_id = merchant.MerchantNo,
                    authority = authority,
                    amount = Convert.ToInt64(payment.Amount),
                    currency = "IRT"
                };

                var content = new StringContent(
                    JsonSerializer.Serialize(verifyData),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync(ZarinPalVerificationUrl, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new GatewayCallbackResultDto
                    {
                        IsSuccess = false,
                        Token = authority,
                        ErrorMessage = $"خطا در ارتباط با زرین‌پال برای verify: {response.StatusCode} - {result}"
                    };
                }

                var verifyResponse = JsonSerializer.Deserialize<ZarinPalVerificationResponse>(
                    result,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (verifyResponse?.data == null)
                {
                    return new GatewayCallbackResultDto
                    {
                        IsSuccess = false,
                        Token = authority,
                        ErrorMessage = "پاسخ verify زرین‌پال نامعتبر است."
                    };
                }

                if (verifyResponse.data.code == 100 || verifyResponse.data.code == 101)
                {
                    return new GatewayCallbackResultDto
                    {
                        IsSuccess = true,
                        RefNumber = verifyResponse.data.ref_id?.ToString(),
                        Token = authority,
                        Description = $"{verifyResponse.data.code}--{verifyResponse.data.ref_id}"
                    };
                }

                return new GatewayCallbackResultDto
                {
                    IsSuccess = false,
                    Token = authority,
                    Description = verifyResponse.data.code.ToString(),
                    ErrorMessage = Resource.Notification.Unsuccess
                };
            }
            catch (Exception ex)
            {
                return new GatewayCallbackResultDto
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        private static bool IsValidCallbackUrl(string callbackUrl, long? paymentId)
        {
            if (string.IsNullOrWhiteSpace(callbackUrl) || paymentId == null)
                return false;

            if (!Uri.TryCreate(callbackUrl, UriKind.Absolute, out var uri))
                return false;

            if (!string.Equals(uri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
                return false;

            if (!string.Equals(uri.Host, AllowedCallbackHost, StringComparison.OrdinalIgnoreCase))
                return false;

            var expectedPath = $"{AllowedCallbackPathPrefix}{paymentId}";
            var actualPath = uri.AbsolutePath.TrimEnd('/');

            return string.Equals(actualPath, expectedPath, StringComparison.Ordinal);
        }
    }
}