using Application.Common.Dto.Field;

namespace Application.Services.Filing.PictureSrv.Dto
{
    public class PictureVDto : Id_FieldDto
    {
        public string Url { get; set; }
        public string OrginalName { get; set; }
        public string BaseUrl { get; set; }

        public string GuidName { get; set; }
        public string Extension { get; set; }
    }
}
