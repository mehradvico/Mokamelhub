using Entities.Entities.CommonField;

namespace Entities.Entities
{
    public class Banner : FullName_Field
    {

        public string Url { get; set; }
        public int Priority { get; set; }
        public long? CategoryId { get; set; }
        public long? PictureId { get; set; }
        public long? Picture2Id { get; set; }
        public int ClickCount { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public Category Category { get; set; }
        public Picture Picture { get; set; }
        public Picture Picture2 { get; set; }
    }
}
