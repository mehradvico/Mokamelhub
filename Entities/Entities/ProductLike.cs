using Entities.Entities.CommonField;
using Entities.Entities.Security;

namespace Entities.Entities
{
    public class ProductLike : Id_Field
    {
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
