using Application.Common.Interface;
using Application.Services.Order.DeliveryDistanceSrv.Dto;
using Application.Services.Order.DeliverySrv.Dto;
using Entities.Entities;

namespace Application.Services.Order.DeliveryDistanceSrv.iface
{
    public interface IDeliveryDistanceService : ICommonSrv<DeliveryDistance, DeliveryDistanceDto>
    {
        DeliveryDistanceSearchDto Search(DeliveryDistanceInputDto baseSearchDto);
    }
}
