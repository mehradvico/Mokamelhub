using Application.Common.Dto.Result;
using Application.Services.Order.SnappPaySrv.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Order.SnappPaySrv.Iface
{
    public interface ISnappPayService
    {
        Task<BaseResultDto<SnappPayEligibilityResponse>> GetEligibilityAsync(double amountToman, IEnumerable<string> paymentMethodTypes = null);
        Task<BaseResultDto<SnappPayOrderOperationResultDto>> GetOrderStatusAsync(string orderId, bool refresh);
        Task<BaseResultDto<SnappPayOrderOperationResultDto>> UpdateOrderAsync(string orderId, SnappPayUpdateOrderDto dto);
        Task<BaseResultDto<SnappPayOrderOperationResultDto>> CancelOrderAsync(string orderId, SnappPayCancelOrderDto dto);
    }
}
