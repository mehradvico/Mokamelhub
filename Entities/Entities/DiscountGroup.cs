using Entities.Entities.CommonField;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class DiscountGroup : Name_Field
    {
        public string Label { get; set; }
        public bool Active { get; set; }
        public long? PictureId { get; set; }
        public Picture Picture { get; set; }
        public ICollection<Discount> Discounts { get; set; }
    }
}
