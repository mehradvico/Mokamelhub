using Entities.Entities.CommonField;
using System;

namespace Entities.Entities
{
    public class CartItem : Id_Field
    {

        public DateTime CreateDate { get; set; }
        public long CartStoreId { get; set; }
        public long ProductItemId { get; set; }
        public int Count { get; set; }
        public CartStore CartStore { get; set; }
        public ProductItem ProductItem { get; set; }

    }
}
