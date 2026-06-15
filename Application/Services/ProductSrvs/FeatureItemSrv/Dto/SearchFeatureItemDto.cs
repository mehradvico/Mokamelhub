using Application.Common.Dto.Field;

namespace Application.Services.ProductSrvs.FeatureSrv.Dto
{
    public class SearchFeatureItemDto : Name_FieldDto
    {
        public long FeatureId { get; set; }
        public string FeatureName { get; set; }
    }
}
