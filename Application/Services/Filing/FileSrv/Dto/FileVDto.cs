using Application.Common.Dto.Field;

namespace Application.Services.Filing.FileSrv.Dto
{
    public class FileVDto : Id_FieldDto
    {
        public string Url { get; set; }
        public string OrginalName { get; set; }

    }
}
