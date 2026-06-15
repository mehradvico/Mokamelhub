using Application.Common.Dto.Field;
using Application.Services.Filing.PictureSrv.Dto;
using System.Runtime.Serialization;

namespace Application.Services.Content.PostPictureSrv.Dto
{
    public class PostPictureDto : Id_FieldDto
    {

        public long PostId { get; set; }
        public long PictureId { get; set; }

        public string Name { get; set; }
        public string Label { get; set; }
        [IgnoreDataMember]
        public PictureVDto Picture { get; set; }

    }
}
