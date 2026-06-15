using Application.Common.Dto.Field;

namespace Application.Services.ProductSrvs.VarietySrv.Dto
{
    public class VarietyShowVDto : Name_FieldDto
    {
        public string Description { get; set; }
        public string Label { get; set; }

    }
}
