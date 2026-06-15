using Application.Common.Dto.Field;

namespace Application.Services.Order.DeliveryDistanceSrv.Dto
{
    public class DeliveryDistanceVDto : Id_FieldDto
    {
        public int FromD { get; set; }
        public int ToD { get; set; }
        public double Price { get; set; }
        public long DeliveryId { get; set; }

    }
}
