using Application.Services.ProductSrvs.VarietySrv.Dto;
using System.Collections.Generic;

namespace Application.Services.ProductSrvs.ProductItemSrv.Dto
{
    public class ProductItemForProductDto
    {

        public VarietyShowVDto Variety1 { get; set; }
        public VarietyShowVDto Variety2 { get; set; }
        public List<ProductItemForProductVDto> ProductItems { get; set; } = new List<ProductItemForProductVDto>();
    }
}
