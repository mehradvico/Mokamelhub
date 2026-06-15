using Application.Common.Dto.Input;
using Application.Services.Order.DeliveryDistanceSrv.iface;

namespace Application.Services.Order.DeliveryDistanceSrv.Dto
{
    public class DeliveryDistanceInputDto : BaseInputDto, IDeliveryDistanceSearchFields
    {
        public long DeliveryId { get; set; }
    }
}
