namespace Entities.Entities
{
    public class ProductComment : Comment
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
