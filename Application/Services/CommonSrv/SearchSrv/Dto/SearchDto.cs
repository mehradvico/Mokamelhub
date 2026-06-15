using Application.Services.CategorySrv.Dto;
using Application.Services.ProductSrvs.BrandSrv.Dto;
using Application.Services.ProductSrvs.FeatureSrv.Dto;
using Application.Services.ProductSrvs.ProductSrv.Dto;
using Application.Services.ProductSrvs.StoreSrv.Dto;
using System.Collections.Generic;

namespace Application.Services.CommonSrv.SearchSrv.Dto
{
    public class SearchDto
    {
        public List<SearchProductDto> Products { get; set; }
        public List<SearchCategoryDto> Categories { get; set; }
        public List<SearchBrandDto> Brands { get; set; }
        public List<SearchFeatureItemDto> Feature { get; set; }
        public List<SearchStoreDto> Stores { get; set; }
    }

}
