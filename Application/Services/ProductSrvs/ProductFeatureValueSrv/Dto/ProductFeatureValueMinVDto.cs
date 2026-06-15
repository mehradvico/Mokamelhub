using Application.Common.Dto.Field;
using Application.Services.ProductSrvs.FeatureSrv.Dto;

namespace Application.Services.ProductSrvs.ProductFeatureValueSrv.Dto
{
    public class ProductFeatureValueMinVDto : Name_FieldDto
    {
        public FeatureItemVDto FeatureItem { get; set; }
    }
}
