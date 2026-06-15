using Entities.Entities.CommonField;

namespace Entities.Entities
{
    public class VarietyItem : Name_Field
    {
        public string Label { get; set; }
        public long VarietyId { get; set; }
        public Variety Variety { get; set; }
        public bool Deleted { get; set; }
    }
}
