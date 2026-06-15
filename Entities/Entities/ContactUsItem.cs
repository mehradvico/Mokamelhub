using Entities.Entities.CommonField;

namespace Entities.Entities
{
    public class ContactUsItem : Id_Field
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public long ContactUsId { get; set; }
        public ContactUs ContactUs { get; set; }
    }
}
