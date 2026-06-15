namespace Entities.Entities
{
    public class StoreComment : Comment
    {
        public long StoreId { get; set; }
        public Store Store { get; set; }
    }
}
