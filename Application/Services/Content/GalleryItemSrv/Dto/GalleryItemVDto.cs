using Application.Common.Dto.Field;
using Application.Services.Filing.PictureSrv.Dto;

namespace Application.Services.Content.GalleryItemSrv.Dto
{
    public class GalleryItemVDto : FullName_FieldDto
    {

        public string Link { get; set; }
        public long? PictureId { get; set; }
        public PictureVDto Picture { get; set; }
        public long GalleryId { get; set; }
    }
}
