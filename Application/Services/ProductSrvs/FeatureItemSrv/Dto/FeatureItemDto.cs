using Application.Common.Dto.Field;

namespace Application.Services.ProductSrvs.FeatureItemSrv.Dto
{
    public class FeatureItemDto : Name_FieldDto
    {
        public long FeatureId { get; set; }
        public int Priority { get; set; }
    }
}
