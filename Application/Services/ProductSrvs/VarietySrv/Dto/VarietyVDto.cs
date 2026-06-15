using Application.Common.Dto.Field;
using Application.Services.ProductSrvs.VarietyItemSrv.Dto;
using System.Collections.Generic;

namespace Application.Services.ProductSrvs.VarietySrv.Dto
{
    public class VarietyVDto : Name_FieldDto
    {
        public string Description { get; set; }
        public int Priority { get; set; }
        public bool InSearch { get; set; }
        public string Label { get; set; }
        public List<VarietyItemDto> VarietyItems { get; set; }

    }
}
