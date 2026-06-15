using Application.Services.CategorySrv.Dto;
using Application.Services.ProductSrvs.VarietySrv.Dto;
using System.Collections.Generic;

namespace Application.Services.ProductSrvs.CategoryVariety.Dto
{
    public class CategoryVarietyDto
    {
        public CategoryDto CategoryDto { get; set; }
        public List<VarietyDto> VarietyDto { get; set; }
    }
}
