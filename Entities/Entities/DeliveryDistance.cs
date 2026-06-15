using Entities.Entities.CommonField;

namespace Entities.Entities
{
    public class DeliveryDistance : Id_Field
    {
        public double FromD { get; set; }
        public double ToD { get; set; }
        public double Price { get; set; }
        public long DeliveryId { get; set; }

        public Delivery Delivery { get; set; }
    }
}
