using Application.Services.CategorySrv.Dto;
using Application.Services.ProductSrvs.FeatureSrv.Dto;
using System.Collections.Generic;

namespace Application.Services.ProductSrvs.FeatureItemSrv.Dto
{
    public class CategoryFeatureDto : CategoryVDto
    {
        public List<FeatureVDto> Features { get; set; }

    }
}
