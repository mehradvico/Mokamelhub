using Application.Common.Enumerable;
using Application.Services.Order.PaymentGatewaySrv.Dto;
using Application.Services.Order.PaymentGatewaySrv.Iface;
using Application.Services.Order.ProductOrderSrv.Dto;
using Application.Services.Order.SnappPaySrv;
using Application.Services.Order.SnappPaySrv.Dto;
using Application.Services.Order.SnappPaySrv.Iface;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Order.PaymentGatewaySrv.Gateways
{
    public class SnappPayGateway : IPaymentGateway
    {
        public MerchantEnum Provider => MerchantEnum.snapppay;

        private readonly ISnappPayClient _client;
        private readonly ISnappPayOrderBuilder _orderBuilder;
        private readonly SnappPayOptions _options;

        public SnappPayGateway(
            ISnappPayClient client,
            ISnappPayOrderBuilder orderBuilder,
            IOptions<SnappPayOptions> options)
        {
            _client = client;
            _orderBuilder = orderBuilder;
            _options = options.Value;
        }

        public async Task<GatewayStartResultDto> StartAsync(PaymentStartDto dto, Merchant merchant)
        {
            try
            {
                var orderId = dto.ProductOrderId ?? dto.CallBackId;
                if (string.IsNullOrWhiteSpace(orderId))
                    return Failure("سفارش مرتبط با پرداخت اسنپ‌پی مشخص نیست.");

                if (string.IsNullOrWhiteSpace(dto.CallbackUrl) || !Uri.TryCreate(dto.CallbackUrl, UriKind.Absolute, out var callbackUri) || callbackUri.Scheme != Uri.UriSchemeHttps)
                    return Failure("آدرس بازگشت اسنپ‌پی باید یک آدرس HTTPS معتبر باشد.");

                var order = await _orderBuilder.LoadOrderAsync(orderId);
                if (order == null)
                    return Failure("سفارش مرتبط با پرداخت اسنپ‌پی پیدا نشد.");

                var payload = _orderBuilder.Build(order);
                var expectedToman = payload.ExpectedAmountRial / 10d;
                if (Math.Abs(dto.Amount - expectedToman) >= 1d)
                    return Failure("مبلغ پرداخت با جزئیات سفارش اسنپ‌پی تطابق ندارد.");

                var transactionId = CreateTransactionId(dto.PaymentId);
                var request = new SnappPayPaymentTokenRequest
                {
                    Amount = payload.Request.Amount,
                    CartList = payload.Request.CartList,
                    DiscountAmount = payload.Request.DiscountAmount,
                    ExternalSourceAmount = payload.Request.ExternalSourceAmount,
                    Mobile = NormalizeMobile(order.User?.Mobile ?? dto.User?.Mobile),
                    ForcedPaymentMethodTypes = _options.PaymentMethodTypes?.Where(x => !string.IsNullOrWhiteSpace(x)).ToList(),
                    ReturnUrl = dto.CallbackUrl,
                    TransactionId = transactionId
                };

                var response = await _client.CreatePaymentTokenAsync(request);
                if (!response.IsSuccess || string.IsNullOrWhiteSpace(response.Data?.PaymentToken) || string.IsNullOrWhiteSpace(response.Data.PaymentPageUrl))
                    return Failure(response.ErrorMessage ?? "دریافت توکن پرداخت اسنپ‌پی ناموفق بود.");

                return new GatewayStartResultDto
                {
                    IsSuccess = true,
                    PaymentIsLink = true,
                    PaymentUrl = response.Data.PaymentPageUrl,
                    Token = response.Data.PaymentToken,
                    GatewayOrderId = transactionId,
                    GatewayStatus = "PENDING",
                    GatewayAmountRial = payload.ExpectedAmountRial
                };
            }
            catch (Exception ex)
            {
                return Failure(ex.Message);
            }
        }

        public async Task<GatewayCallbackResultDto> CallbackAsync(Payment payment, Merchant merchant, HttpRequest request)
        {
            if (payment == null || string.IsNullOrWhiteSpace(payment.Token) || string.IsNullOrWhiteSpace(payment.GatewayTransactionId))
                return CallbackFailure("اطلاعات تراکنش اسنپ‌پی ناقص است.", false, payment?.GatewayStatus);

            if (!request.HasFormContentType)
                return CallbackFailure("کالبک اسنپ‌پی باید با فرم POST ارسال شود.", false, payment.GatewayStatus);

            var form = await request.ReadFormAsync();
            var transactionId = form["transactionId"].ToString();
            var state = form["state"].ToString();
            var amountText = form["amount"].ToString();

            if (!string.Equals(transactionId, payment.GatewayTransactionId, StringComparison.Ordinal))
                return CallbackFailure("شناسه تراکنش کالبک با سفارش تطابق ندارد.", false, payment.GatewayStatus);

            if (!long.TryParse(amountText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var callbackAmount) ||
                payment.GatewayAmountRial == null || callbackAmount != payment.GatewayAmountRial.Value)
                return CallbackFailure("مبلغ کالبک اسنپ‌پی با سفارش تطابق ندارد.", false, payment.GatewayStatus);

            if (!string.Equals(state, "OK", StringComparison.OrdinalIgnoreCase))
                return CallbackFailure("پرداخت اسنپ‌پی ناموفق یا توسط کاربر لغو شد.", true, "FAILED");

            if (string.Equals(payment.GatewayStatus, "SETTLE", StringComparison.OrdinalIgnoreCase))
                return CallbackSuccess(payment, transactionId, callbackAmount, payment.GatewayVerifiedAt, payment.GatewaySettledAt);

            var verifiedAt = payment.GatewayVerifiedAt;
            var verify = await _client.VerifyAsync(payment.Token);
            var currentStatus = verify.IsSuccess ? "VERIFY" : null;
            if (verify.IsSuccess)
                verifiedAt ??= DateTime.Now;

            if (!verify.IsSuccess)
            {
                var statusResult = await _client.GetPaymentStatusAsync(payment.Token);
                if (!statusResult.IsSuccess)
                    return CallbackFailure(verify.ErrorMessage ?? statusResult.ErrorMessage, false, payment.GatewayStatus);

                currentStatus = NormalizeStatus(statusResult.Data?.Status);
                if (!AmountMatches(statusResult.Data, payment.GatewayAmountRial))
                    return CallbackFailure("مبلغ استعلام اسنپ‌پی با سفارش تطابق ندارد.", true, "FAILED");

                if (currentStatus == "SETTLE")
                    return CallbackSuccess(payment, transactionId, callbackAmount, verifiedAt, DateTime.Now);

                if (currentStatus == "CANCEL" || currentStatus == "REVERT")
                    return CallbackFailure($"تراکنش اسنپ‌پی در وضعیت {currentStatus} قرار دارد.", true, currentStatus);

                if (currentStatus == "PENDING")
                {
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    verify = await _client.VerifyAsync(payment.Token);
                    if (verify.IsSuccess)
                    {
                        currentStatus = "VERIFY";
                        verifiedAt ??= DateTime.Now;
                    }
                    else
                    {
                        return CallbackFailure(verify.ErrorMessage, false, "PENDING");
                    }
                }
            }

            if (currentStatus != "VERIFY")
                return CallbackFailure($"وضعیت تراکنش اسنپ‌پی برای تسویه معتبر نیست: {currentStatus}", false, currentStatus);

            var settle = await _client.SettleAsync(payment.Token);
            if (settle.IsSuccess)
                return CallbackSuccess(payment, transactionId, callbackAmount, verifiedAt, DateTime.Now);

            var finalStatus = await _client.GetPaymentStatusAsync(payment.Token);
            if (finalStatus.IsSuccess && AmountMatches(finalStatus.Data, payment.GatewayAmountRial))
            {
                var status = NormalizeStatus(finalStatus.Data?.Status);
                if (status == "SETTLE")
                    return CallbackSuccess(payment, transactionId, callbackAmount, verifiedAt, DateTime.Now);

                if (status == "CANCEL" || status == "REVERT")
                    return CallbackFailure($"تراکنش اسنپ‌پی در وضعیت {status} قرار دارد.", true, status);

                return CallbackFailure(settle.ErrorMessage, false, status);
            }

            return CallbackFailure(settle.ErrorMessage ?? finalStatus.ErrorMessage, false, "VERIFY");
        }

        private static GatewayStartResultDto Failure(string message) => new()
        {
            IsSuccess = false,
            ErrorMessage = message
        };

        private static GatewayCallbackResultDto CallbackSuccess(Payment payment, string transactionId, long amountRial, DateTime? verifiedAt, DateTime? settledAt) => new()
        {
            IsSuccess = true,
            IsFinal = true,
            RefNumber = transactionId,
            Token = payment.Token,
            GatewayStatus = "SETTLE",
            GatewayAmountRial = amountRial,
            VerifiedAt = verifiedAt,
            SettledAt = settledAt,
            Description = "پرداخت اسنپ‌پی با موفقیت Verify و Settle شد."
        };

        private static GatewayCallbackResultDto CallbackFailure(string message, bool isFinal, string status) => new()
        {
            IsSuccess = false,
            IsFinal = isFinal,
            ErrorMessage = message,
            Description = message,
            GatewayStatus = status
        };

        private static bool AmountMatches(SnappPayPaymentStatusResponse status, long? expectedAmount) =>
            status != null && expectedAmount.HasValue && status.Amount == expectedAmount.Value;

        private static string NormalizeStatus(string status) => status?.Trim().ToUpperInvariant();

        private static string NormalizeMobile(string mobile)
        {
            var digits = new string((mobile ?? string.Empty).Where(char.IsDigit).ToArray());
            if (digits.StartsWith("0098")) digits = digits[4..];
            else if (digits.StartsWith("98")) digits = digits[2..];
            if (digits.StartsWith("0")) digits = digits[1..];

            if (digits.Length != 10 || !digits.StartsWith("9"))
                throw new InvalidOperationException("شماره موبایل سفارش برای اسنپ‌پی معتبر نیست.");

            return $"+98{digits}";
        }

        private static string CreateTransactionId(long paymentId)
        {
            const string alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var value = paymentId;
            var builder = new StringBuilder();
            do
            {
                builder.Insert(0, alphabet[(int)(value % 36)]);
                value /= 36;
            } while (value > 0);

            var encoded = builder.ToString().PadLeft(4, '0');
            var result = $"M{encoded}";
            if (result.Length > 10)
                throw new InvalidOperationException("شناسه پرداخت برای ساخت transactionId اسنپ‌پی بیش از حد بزرگ است.");

            return result;
        }
    }
}
