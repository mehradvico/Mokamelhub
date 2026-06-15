using Application.Services.ProductSrvs.FeatureSrv.Dto;
using System.Collections.Generic;

namespace Application.Services.ProductSrvs.ProductFeatureValueSrv.Dto
{
    public class ProductFeatureVDto : FeatureMinVDto
    {
        public List<ProductFeatureValueMinVDto> ProductFeatureValues { get; set; } = new List<ProductFeatureValueMinVDto>();
    }
}
