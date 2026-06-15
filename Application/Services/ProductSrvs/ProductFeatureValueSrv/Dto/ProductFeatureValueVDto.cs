using Application.Common.Dto.Field;
using Application.Services.ProductSrvs.FeatureSrv.Dto;

namespace Application.Services.ProductSrvs.ProductFeatureValueSrv.Dto
{
    public class ProductFeatureValueVDto : Name_FieldDto
    {
        public long ProductId { get; set; }
        public long? FeatureItemId { get; set; }
        public long FeatureId { get; set; }
        public FeatureMinVDto Feature { get; set; }
        public FeatureItemVDto FeatureItem { get; set; }
    }
}
