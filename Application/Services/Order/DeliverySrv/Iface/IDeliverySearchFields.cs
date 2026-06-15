using Application.Common.Enumerable;

namespace Application.Services.Order.DeliverySrv.iface
{
    public interface IDeliverySearchFields
    {
        public DeliveryTypeEnum? DeliveryTypeEnum { get; set; }
        public long? StoreId { get; set; }

    }
}
