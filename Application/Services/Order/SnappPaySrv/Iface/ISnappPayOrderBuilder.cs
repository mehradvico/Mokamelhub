using Application.Services.Order.SnappPaySrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.Order.SnappPaySrv.Iface
{
    public interface ISnappPayOrderBuilder
    {
        Task<ProductOrder> LoadOrderAsync(string orderId, bool tracking = false);
        SnappPayOrderPayload Build(ProductOrder order);
        void Recalculate(ProductOrder order);
    }
}
