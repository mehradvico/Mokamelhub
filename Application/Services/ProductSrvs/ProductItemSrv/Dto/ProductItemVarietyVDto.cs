using Application.Services.ProductSrvs.VarietyItemSrv.Dto;
using Application.Services.ProductSrvs.VarietySrv.Dto;
using System.Collections.Generic;

namespace Application.Services.ProductSrvs.ProductItemSrv.Dto
{
    public class ProductItemVarietyVDto
    {
        public VarietyShowVDto Variety { get; set; }
        public List<VarietyItemShowVDto> VarietyItems { get; set; } = new List<VarietyItemShowVDto>();
    }

}
