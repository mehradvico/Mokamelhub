using Application.Common.Dto.Field;
using Application.Services.ProductSrvs.ProductItemSrv.Dto;
using System;

namespace Application.Services.Order.CartItemSrv.Dto
{
    public class CartItemVDto : Id_FieldDto
    {

        public DateTime CreateDate { get; set; }
        public long CartStoreId { get; set; }
        public long ProductItemId { get; set; }
        public double PriceChanged { get; set; }
        public bool Deleted { get; set; }
        public int Count { get; set; }
        public ProductItemVDto ProductItem { get; set; }

    }
}
