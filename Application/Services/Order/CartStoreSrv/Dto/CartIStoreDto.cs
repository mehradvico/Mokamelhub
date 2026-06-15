using Application.Common.Dto.Field;

namespace Application.Services.Order.CartStoreSrv.Dto
{
    public class CartStoreDto : Id_FieldDto
    {
        public long CartId { get; set; }
        public int ItemCount { get; set; }
        public double BasePrice { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public long StoreId { get; set; }
        public long? DeliveryId { get; set; }
        public double DeliveryPrice { get; set; }
        public double PaymentPrice { get; set; }

    }
}
