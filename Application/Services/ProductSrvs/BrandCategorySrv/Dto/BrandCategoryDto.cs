using Application.Services.CategorySrv.Dto;
using Application.Services.ProductSrvs.BrandSrv.Dto;
using System.Collections.Generic;

namespace Application.Services.ProductSrvs.BrandCategorySrv.Dto
{
    public class BrandCategoryDto
    {
        public BrandDto BrandDto { get; set; }
        public List<CategoryDto> CategoryDto { get; set; }
    }
}
