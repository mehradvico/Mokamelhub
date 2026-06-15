using Entities.Entities.CommonField;

namespace Entities.Entities
{
    public class PostPicture : Name_Field
    {
        public long PostId { get; set; }
        public long PictureId { get; set; }
        public string Label { get; set; }
        public Post Post { get; set; }
        public Picture Picture { get; set; }
    }
}
