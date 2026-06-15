using Application.Common.Dto.Field;

namespace Application.Services.ProductSrvs.VarietyItemSrv.Dto
{
    public class VarietyItemVDto : Name_FieldDto
    {

        public string Label { get; set; }
        public long VarietyId { get; set; }
        public string VarietyName { get; set; }

    }
}
