using Application.Common.Dto.Field;
using Application.Services.Filing.PictureSrv.Dto;

namespace Application.Services.ProductSrvs.DiscountGroupSrv.Dto
{
    public class DiscountGroupDto : Name_FieldDto
    {
        public string Label { get; set; }
        public bool Active { get; set; }
        public long? PictureId { get; set; }
        public PictureVDto Picture { get; set; }
    }
}
