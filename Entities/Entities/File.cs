using Entities.Entities.CommonField;
using System;

namespace Entities.Entities
{
    public class File : Name_Field
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
