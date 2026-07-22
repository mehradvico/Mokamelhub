using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Services.Order.SnappPaySrv.Dto
{
    public class SnappPayApiResponse<T>
    {
        public bool Successful { get; set; }
        public T Response { get; set; }
        public SnappPayErrorData ErrorData { get; set; }
    }

    public class SnappPayErrorData
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public class SnappPayCallResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public int? ErrorCode { get; set; }
        public int HttpStatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsTimeout { get; set; }

        public static SnappPayCallResult<T> Success(T data, int statusCode) => new()
        {
            IsSuccess = true,
            Data = data,
            HttpStatusCode = statusCode
        };

        public static SnappPayCallResult<T> Failure(string message, int statusCode = 0, int? errorCode = null, bool isTimeout = false) => new()
        {
            IsSuccess = false,
            ErrorMessage = message,
            HttpStatusCode = statusCode,
            ErrorCode = errorCode,
            IsTimeout = isTimeout
        };
    }

    public class SnappPayOAuthResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }

    public class SnappPayEligibilityResponse
    {
        public bool Eligible { get; set; }

        [JsonPropertyName("title_message")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class SnappPayPaymentTokenResponse
    {
        public string PaymentToken { get; set; }
        public string PaymentPageUrl { get; set; }
    }

    public class SnappPayTransactionResponse
    {
        public string TransactionId { get; set; }
    }

    public class SnappPayPaymentStatusResponse
    {
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public long Amount { get; set; }
    }

    public class SnappPayCartItemRequest
    {
        public long Amount { get; set; }
        public string Category { get; set; }
        public int Count { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public int CommissionType { get; set; }
    }

    public class SnappPayCartRequest
    {
        public long CartId { get; set; }
        public List<SnappPayCartItemRequest> CartItems { get; set; } = new();
        public bool IsShipmentIncluded { get; set; }
        public bool IsTaxIncluded { get; set; }
        public long ShippingAmount { get; set; }
        public long TaxAmount { get; set; }
        public long TotalAmount { get; set; }
    }

    public class SnappPayOrderRequest
    {
        public long Amount { get; set; }
        public List<SnappPayCartRequest> CartList { get; set; } = new();
        public long DiscountAmount { get; set; }
        public long ExternalSourceAmount { get; set; }
    }

    public class SnappPayPaymentTokenRequest : SnappPayOrderRequest
    {
        public string Mobile { get; set; }
        public List<string> ForcedPaymentMethodTypes { get; set; }

        [JsonPropertyName("returnURL")]
        public string ReturnUrl { get; set; }

        public string TransactionId { get; set; }
    }

    public class SnappPayUpdateRequest : SnappPayOrderRequest
    {
        public string PaymentToken { get; set; }
    }

    public class SnappPayTokenRequest
    {
        public string PaymentToken { get; set; }
    }

    public class SnappPayOrderPayload
    {
        public SnappPayOrderRequest Request { get; set; }
        public long ExpectedAmountRial { get; set; }
    }

    public class SnappPayItemChangeDto
    {
        public long ProductOrderItemId { get; set; }
        public int Count { get; set; }
        public bool Deleted { get; set; }
    }

    public class SnappPayUpdateOrderDto
    {
        public bool Confirmed { get; set; }
        public List<SnappPayItemChangeDto> Items { get; set; } = new();
    }

    public class SnappPayCancelOrderDto
    {
        public bool Confirmed { get; set; }
    }

    public class SnappPayOrderOperationResultDto
    {
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public long AmountRial { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
