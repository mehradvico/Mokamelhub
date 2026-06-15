using Entities.Entities.CommonField;

namespace Entities.Entities
{
    public class SmsProvider : Name_Field
    {
        public string SiteUrl { get; set; }
        public string ServiceUrl { get; set; }
        public string Label { get; set; }
    }
}
