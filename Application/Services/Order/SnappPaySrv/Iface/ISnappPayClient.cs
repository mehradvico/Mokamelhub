using Application.Services.Order.SnappPaySrv.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Order.SnappPaySrv.Iface
{
    public interface ISnappPayClient
    {
        Task<SnappPayCallResult<SnappPayEligibilityResponse>> GetEligibilityAsync(long amountRial, IEnumerable<string> paymentMethodTypes = null);
        Task<SnappPayCallResult<SnappPayPaymentTokenResponse>> CreatePaymentTokenAsync(SnappPayPaymentTokenRequest request);
        Task<SnappPayCallResult<SnappPayTransactionResponse>> VerifyAsync(string paymentToken);
        Task<SnappPayCallResult<SnappPayTransactionResponse>> SettleAsync(string paymentToken);
        Task<SnappPayCallResult<SnappPayPaymentStatusResponse>> GetPaymentStatusAsync(string paymentToken);
        Task<SnappPayCallResult<SnappPayTransactionResponse>> UpdateAsync(SnappPayUpdateRequest request);
        Task<SnappPayCallResult<SnappPayTransactionResponse>> CancelAsync(string paymentToken);
    }
}
