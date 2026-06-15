using Application.Services.ProductSrvs.ProductSrv.Dto;
using System.Collections.Generic;

namespace Application.Services.ProductSrvs.ProductRelateSrv.Dto
{
    public class ProductRelateVDto
    {
        public string Label { get; set; }

        public long ProductId { get; set; }
        public ProductVDto Product { get; set; }
        public List<ProductVDto> RelatedProductList { get; set; }
    }
}
