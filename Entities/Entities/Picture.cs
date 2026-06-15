using Entities.Entities.CommonField;
using System;

namespace Entities.Entities
{
    public class Picture : Name_Field
    {
        public string GuidName { get; set; }
        public string Url { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public string DirectUrl { get; set; }
        public string OrginalName { get; set; }
        public long Size { get; set; }
        public DateTime CreateDate { get; set; }



    }
}
