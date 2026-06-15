using Application.Services.ProductSrvs.ProductSrv.Dto;
using System.Collections.Generic;

namespace Application.Services.ProductSrvs.ProductItemSrv.Dto
{
    public class ProductItemListUpdateDto
    {
        public long StoreId { get; set; }
        public long ProductId { get; set; }
        public ProductMinVDto Product { get; set; }
        public List<ProductItemDto> ProductItems { get; set; } = new List<ProductItemDto>();

    }
}
