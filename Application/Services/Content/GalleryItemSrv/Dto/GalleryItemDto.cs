using Application.Common.Dto.Field;
using Application.Services.Content.GallerySrv.Dto;
using Application.Services.Filing.PictureSrv.Dto;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.Services.Content.GalleryItemSrv.Dto
{
    public class GalleryItemDto : FullName_FieldDto
    {
        [Display(Name = nameof(Resource.Field.Link), ResourceType = typeof(Resource.Field))]

        public string Link { get; set; }
        public bool Active { get; set; }
        public int Priority { get; set; }

        public long? PictureId { get; set; }
        [IgnoreDataMember]
        public PictureVDto Picture { get; set; }
        public long GalleryId { get; set; }
        [IgnoreDataMember]
        public GalleryVDto Gallery { get; set; }
    }
}
