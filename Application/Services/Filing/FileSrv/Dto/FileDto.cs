using Application.Common.Dto.Field;
using System;

namespace Application.Services.Filing.FileSrv.Dto
{
    public class FileDto : Name_FieldDto
    {
        public string OrginalName { get; set; }

        public bool Protected { get; set; }
        public string Url { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public string DirectUrl { get; set; }
        public long Size { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
