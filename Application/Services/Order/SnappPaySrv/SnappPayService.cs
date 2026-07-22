using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.Order.SnappPaySrv.Dto;
using Application.Services.Order.SnappPaySrv.Iface;
using Application.Services.Order.ProductOrderSrv.Iface;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Order.SnappPaySrv
{
    public class SnappPayService : ISnappPayService
    {
        private readonly IDataBaseContext _context;
        private readonly ISnappPayClient _client;
        private readonly ISnappPayOrderBuilder _orderBuilder;
        private readonly IProductOrderService _productOrderService;

        public SnappPayService(
            IDataBaseContext context,
            ISnappPayClient client,
            ISnappPayOrderBuilder orderBuilder,
            IProductOrderService productOrderService)
        {
            _context = context;
            _client = client;
            _orderBuilder = orderBuilder;
            _productOrderService = productOrderService;
        }

        public async Task<BaseResultDto<SnappPayEligibilityResponse>> GetEligibilityAsync(
            double amountToman,
            IEnumerable<string> paymentMethodTypes = null)
        {
            if (double.IsNaN(amountToman) || double.IsInfinity(amountToman) || amountToman <= 0)
                return Fail<SnappPayEligibilityResponse>("مبلغ ارسالی برای بررسی اسنپ‌پی معتبر نیست.");

            long amountRial;
            try
            {
                amountRial = checked((long)Math.Round((decimal)amountToman * 10m, MidpointRounding.AwayFromZero));
            }
            catch (OverflowException)
            {
                return Fail<SnappPayEligibilityResponse>("مبلغ ارسالی برای بررسی اسنپ‌پی بیش از حد مجاز است.");
            }

            var result = await _client.GetEligibilityAsync(amountRial, paymentMethodTypes);
            return result.IsSuccess
                ? new BaseResultDto<SnappPayEligibilityResponse>(true, result.Data)
                : Fail<SnappPayEligibilityResponse>(result.ErrorMessage, result.ErrorCode ?? 0);
        }

        public async Task<BaseResultDto<SnappPayOrderOperationResultDto>> GetOrderStatusAsync(string orderId, bool refresh)
        {
            var payment = await FindPaymentAsync(orderId, true);
            if (payment == null)
                return Fail<SnappPayOrderOperationResultDto>("تراکنش اسنپ‌پی این سفارش پیدا نشد.");

            if (refresh)
            {
                var status = await _client.GetPaymentStatusAsync(payment.Token);
                if (!status.IsSuccess)
                {
                    payment.GatewayLastError = status.ErrorMessage;
                    await _context.SaveChangesAsync();
                    return Fail<SnappPayOrderOperationResultDto>(status.ErrorMessage, status.ErrorCode ?? 0);
                }

                if (payment.GatewayAmountRial.HasValue && status.Data.Amount != payment.GatewayAmountRial.Value)
                    return Fail<SnappPayOrderOperationResultDto>("مبلغ استعلام اسنپ‌پی با مبلغ ذخیره‌شده سفارش تطابق ندارد.");

                var currentStatus = NormalizeStatus(status.Data.Status);
                if (currentStatus == "PENDING")
                {
                    var verify = await _client.VerifyAsync(payment.Token);
                    if (verify.IsSuccess)
                    {
                        currentStatus = "VERIFY";
                        payment.GatewayVerifiedAt ??= DateTime.Now;
                    }
                }

                if (currentStatus == "VERIFY")
                {
                    var settle = await _client.SettleAsync(payment.Token);
                    if (settle.IsSuccess)
                    {
                        currentStatus = "SETTLE";
                        payment.GatewaySettledAt ??= DateTime.Now;
                    }
                    else
                    {
                        var finalStatus = await _client.GetPaymentStatusAsync(payment.Token);
                        if (finalStatus.IsSuccess)
                            currentStatus = NormalizeStatus(finalStatus.Data.Status);
                    }
                }

                payment.GatewayStatus = currentStatus;
                payment.GatewayTransactionId ??= status.Data.TransactionId;
                payment.GatewayLastError = null;
                if (currentStatus == "SETTLE")
                    payment.IsSuccess = true;
                else if (currentStatus == "CANCEL" || currentStatus == "REVERT")
                    payment.IsSuccess = false;
                await _context.SaveChangesAsync();

                if (currentStatus == "SETTLE" && !string.IsNullOrWhiteSpace(payment.ProductOrderId))
                    await _productOrderService.ProductPaymentCallback(payment.ProductOrderId);
            }

            return Success(payment);
        }

        public async Task<BaseResultDto<SnappPayOrderOperationResultDto>> UpdateOrderAsync(string orderId, SnappPayUpdateOrderDto dto)
        {
            if (dto?.Confirmed != true)
                return Fail<SnappPayOrderOperationResultDto>("تأیید نهایی ادمین برای بروزرسانی سفارش الزامی است.");

            if (dto.Items == null || dto.Items.Count == 0)
                return Fail<SnappPayOrderOperationResultDto>("حداقل یک تغییر آیتم باید ارسال شود.");

            var payment = await FindPaymentAsync(orderId, true);
            if (payment == null)
                return Fail<SnappPayOrderOperationResultDto>("تراکنش اسنپ‌پی این سفارش پیدا نشد.");

            var statusCheck = await EnsureSettledAsync(payment);
            if (statusCheck != null)
                return statusCheck;

            var order = await _orderBuilder.LoadOrderAsync(orderId, true);
            if (order == null)
                return Fail<SnappPayOrderOperationResultDto>("سفارش پیدا نشد.");

            var allItems = order.ProductOrderStores.SelectMany(x => x.ProductOrderItems).ToDictionary(x => x.Id);
            var duplicateId = dto.Items.GroupBy(x => x.ProductOrderItemId).FirstOrDefault(x => x.Count() > 1)?.Key;
            if (duplicateId.HasValue)
                return Fail<SnappPayOrderOperationResultDto>($"آیتم {duplicateId.Value} بیش از یک بار در درخواست ارسال شده است.");

            foreach (var change in dto.Items)
            {
                if (!allItems.TryGetValue(change.ProductOrderItemId, out var item))
                    return Fail<SnappPayOrderOperationResultDto>($"آیتم سفارش با شناسه {change.ProductOrderItemId} پیدا نشد.");

                if (change.Count < 0)
                    return Fail<SnappPayOrderOperationResultDto>("تعداد آیتم نمی‌تواند منفی باشد.");

                if (!change.Deleted && change.Count > item.Count)
                    return Fail<SnappPayOrderOperationResultDto>("در بروزرسانی اسنپ‌پی افزایش تعداد آیتم مجاز نیست.");

                item.Count = change.Deleted ? 0 : change.Count;
                item.Deleted = change.Deleted || change.Count == 0;
                item.Edited = true;
            }

            _orderBuilder.Recalculate(order);
            SnappPayOrderPayload payload;
            try
            {
                payload = _orderBuilder.Build(order);
            }
            catch (Exception ex)
            {
                return Fail<SnappPayOrderOperationResultDto>(ex.Message);
            }

            if (payment.GatewayAmountRial.HasValue && payload.ExpectedAmountRial > payment.GatewayAmountRial.Value)
                return Fail<SnappPayOrderOperationResultDto>("مبلغ سفارش بروزشده نمی‌تواند بیشتر از مبلغ قبلی باشد.");

            var updateRequest = new SnappPayUpdateRequest
            {
                Amount = payload.Request.Amount,
                CartList = payload.Request.CartList,
                DiscountAmount = payload.Request.DiscountAmount,
                ExternalSourceAmount = payload.Request.ExternalSourceAmount,
                PaymentToken = payment.Token
            };

            var update = await _client.UpdateAsync(updateRequest);
            if (!update.IsSuccess)
            {
                payment.GatewayLastError = update.ErrorMessage;
                return Fail<SnappPayOrderOperationResultDto>(update.ErrorMessage, update.ErrorCode ?? 0);
            }

            if (!string.IsNullOrWhiteSpace(update.Data?.TransactionId) &&
                !string.Equals(update.Data.TransactionId, payment.GatewayTransactionId, StringComparison.Ordinal))
                return Fail<SnappPayOrderOperationResultDto>("شناسه تراکنش پاسخ Update با سفارش تطابق ندارد.");

            var editedCode = await _context.Codes.FirstOrDefaultAsync(x => x.Label == ProductOrderStateEnum.ProductOrderState_Edited.ToString());
            if (editedCode != null)
                order.ProductOrderStateId = editedCode.Id;

            payment.GatewayAmountRial = payload.ExpectedAmountRial;
            payment.GatewayUpdatedAt = DateTime.Now;
            payment.GatewayLastError = null;
            payment.GatewayStatus = "SETTLE";
            await _context.SaveChangesAsync();

            return Success(payment);
        }

        public async Task<BaseResultDto<SnappPayOrderOperationResultDto>> CancelOrderAsync(string orderId, SnappPayCancelOrderDto dto)
        {
            if (dto?.Confirmed != true)
                return Fail<SnappPayOrderOperationResultDto>("تأیید نهایی ادمین برای لغو سفارش الزامی است.");

            var payment = await FindPaymentAsync(orderId, true);
            if (payment == null)
                return Fail<SnappPayOrderOperationResultDto>("تراکنش اسنپ‌پی این سفارش پیدا نشد.");

            if (string.Equals(payment.GatewayStatus, "CANCEL", StringComparison.OrdinalIgnoreCase))
                return Success(payment);

            var statusCheck = await EnsureSettledAsync(payment);
            if (statusCheck != null)
                return statusCheck;

            var cancel = await _client.CancelAsync(payment.Token);
            if (!cancel.IsSuccess)
            {
                payment.GatewayLastError = cancel.ErrorMessage;
                await _context.SaveChangesAsync();
                return Fail<SnappPayOrderOperationResultDto>(cancel.ErrorMessage, cancel.ErrorCode ?? 0);
            }

            if (!string.IsNullOrWhiteSpace(cancel.Data?.TransactionId) &&
                !string.Equals(cancel.Data.TransactionId, payment.GatewayTransactionId, StringComparison.Ordinal))
                return Fail<SnappPayOrderOperationResultDto>("شناسه تراکنش پاسخ Cancel با سفارش تطابق ندارد.");

            var order = await _context.ProductOrders.AsTracking().FirstOrDefaultAsync(x => x.Id == orderId);
            if (order != null)
            {
                var canceledCode = await _context.Codes.FirstOrDefaultAsync(x => x.Label == ProductOrderStateEnum.ProductOrderState_Canceled.ToString());
                if (canceledCode != null)
                    order.ProductOrderStateId = canceledCode.Id;
                order.CancelRequestDate = DateTime.Now;
            }

            payment.GatewayStatus = "CANCEL";
            payment.GatewayCanceledAt = DateTime.Now;
            payment.GatewayLastError = null;
            await _context.SaveChangesAsync();
            return Success(payment);
        }

        private async Task<BaseResultDto<SnappPayOrderOperationResultDto>> EnsureSettledAsync(Payment payment)
        {
            if (string.Equals(payment.GatewayStatus, "SETTLE", StringComparison.OrdinalIgnoreCase))
                return null;

            var status = await _client.GetPaymentStatusAsync(payment.Token);
            if (!status.IsSuccess)
                return Fail<SnappPayOrderOperationResultDto>(status.ErrorMessage, status.ErrorCode ?? 0);

            if (payment.GatewayAmountRial.HasValue && status.Data.Amount != payment.GatewayAmountRial.Value)
                return Fail<SnappPayOrderOperationResultDto>("مبلغ استعلام اسنپ‌پی با سفارش تطابق ندارد.");

            payment.GatewayStatus = NormalizeStatus(status.Data.Status);
            if (payment.GatewayStatus != "SETTLE")
                return Fail<SnappPayOrderOperationResultDto>($"عملیات فقط برای تراکنش SETTLE مجاز است؛ وضعیت فعلی {payment.GatewayStatus} است.");

            return null;
        }

        private Task<Payment> FindPaymentAsync(string orderId, bool tracking)
        {
            var query = _context.Payments
                .Include(x => x.Merchant)
                .Where(x => x.Merchant != null &&
                            x.Merchant.BankId == (long)MerchantEnum.snapppay &&
                            (x.ProductOrderId == orderId || x.CallBackId == orderId))
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (tracking)
                query = query.AsTracking();

            return query.FirstOrDefaultAsync();
        }

        private static BaseResultDto<SnappPayOrderOperationResultDto> Success(Payment payment) =>
            new(true, new SnappPayOrderOperationResultDto
            {
                TransactionId = payment.GatewayTransactionId,
                Status = payment.GatewayStatus,
                AmountRial = payment.GatewayAmountRial ?? 0,
                UpdatedAt = payment.GatewayUpdatedAt ?? payment.GatewayCanceledAt ?? payment.GatewaySettledAt
            });

        private static BaseResultDto<T> Fail<T>(string message, int code = 0) =>
            new(false, message ?? "عملیات اسنپ‌پی ناموفق بود.", default, code);

        private static string NormalizeStatus(string status) => status?.Trim().ToUpperInvariant();
    }
}
