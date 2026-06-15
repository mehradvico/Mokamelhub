using Application.Common.Dto.Field;
using System;

namespace Application.Services.Order.CartItemSrv.Dto
{
    public class CartItemDto : Id_FieldDto
    {
        public double Price { get; set; }
        public double BasePrice { get; set; }
        public double DiscountPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public long CartStoreId { get; set; }
        public long ProductItemId { get; set; }
        public double PriceChanged { get; set; }
        public bool Deleted { get; set; }
        public int Count { get; set; }

    }
}
