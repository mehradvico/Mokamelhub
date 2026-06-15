using Application.Common.Dto.Input;
using Application.Common.Enumerable;
using Application.Services.Order.DeliverySrv.iface;

namespace Application.Services.Order.DeliverySrv.Dto
{
    public class DeliveryInputDto : BaseInputDto, IDeliverySearchFields
    {
        public DeliveryTypeEnum? DeliveryTypeEnum { get; set; }
        public long? StoreId { get; set; }
    }
}
