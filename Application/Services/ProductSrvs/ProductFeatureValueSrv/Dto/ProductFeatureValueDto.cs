using Application.Common.Dto.Field;

namespace Application.Services.ProductSrvs.FeatureItemSrv.Dto
{
    public class ProductFeatureValueDto : Name_FieldDto
    {
        public long ProductId { get; set; }
        public long? FeatureItemId { get; set; }
        public long FeatureId { get; set; }

    }
}
