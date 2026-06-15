using Application.Common.Dto.Field;
using Application.Services.Filing.FileSrv.Dto;

namespace Application.Services.Content.PostFileSrv.Dto
{
    public class PostFileVDto : Name_FieldDto
    {
        public long FileId { get; set; }
        public string Label { get; set; }
        public FileVDto File { get; set; }

    }
}
