using Application.Services.ProductSrvs.ProductItemSrv.Dto;
using System.Collections.Generic;

namespace Application.Services.ProductSrvs.VarietyItemSrv.Dto
{
    public class VarietyItemShowVDto
    {
        public VarietyItemMinVDto VarietyItem { get; set; }
        public List<ProductItemShowVDto> ProductItems { get; set; }

    }
}
