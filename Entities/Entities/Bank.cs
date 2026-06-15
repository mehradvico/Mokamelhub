using Entities.Entities.CommonField;

namespace Entities.Entities
{
    public class Bank : Name_Field
    {
        public long? PictureId { get; set; }
        public string Label { get; set; }
        public string PaymentUrl { get; set; }
        public string VerficationUrl { get; set; }
        public string Verfication2Url { get; set; }
        public bool Active { get; set; }
        public Picture Picture { get; set; }
    }
}
