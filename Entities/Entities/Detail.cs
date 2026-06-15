using Entities.Entities.CommonField;

namespace Entities.Entities
{
    public class Detail : Id_Field
    {
        public string Label { get; set; }
        public string UiLabel { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public long TypeId { get; set; }
        public long CategoryId { get; set; }
        public Code Type { get; set; }
        public Category Category { get; set; }

    }
}
