using Entities.Entities.CommonField;

namespace Entities.Entities
{
    public class PostFile : Name_Field
    {
        public long PostId { get; set; }
        public long FileId { get; set; }
        public string Label { get; set; }
        public Post Post { get; set; }
        public File File { get; set; }
    }
}
