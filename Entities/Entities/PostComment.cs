namespace Entities.Entities
{
    public class PostComment : Comment
    {
        public long PostId { get; set; }
        public Post Post { get; set; }
    }
}
