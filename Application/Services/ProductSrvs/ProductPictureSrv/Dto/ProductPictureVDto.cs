using Application.Common.Dto.Field;
using Application.Services.Filing.PictureSrv.Dto;

namespace Application.Services.ProductSrvs.ProductPictureSrv.Dto
{
    public class ProductPictureVDto : Id_FieldDto
    {
        public long ProductId { get; set; }
        public long PictureId { get; set; }
        public string Label { get; set; }
        public PictureVDto Picture { get; set; }
    }
}
