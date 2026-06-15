using Entities.Entities.CommonField;

namespace Entities.Entities
{
    public class ContactUsGroup : Name_Field
    {
        public string Label { get; set; }
        public bool Active { get; set; }
        public int Priority { get; set; }
        public string Roles { get; set; }

    }
}
