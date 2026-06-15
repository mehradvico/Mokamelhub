using Application.Common.Dto.Field;
using Application.Services.Filing.FileSrv.Dto;
using System.Runtime.Serialization;

namespace Application.Services.Content.PostFileSrv.Dto
{
    public class PostFileDto : Id_FieldDto
    {
        public long PostId { get; set; }
        public long FileId { get; set; }

        public string Name { get; set; }
        public string Label { get; set; }
        [IgnoreDataMember]
        public FileVDto File { get; set; }

    }
}
