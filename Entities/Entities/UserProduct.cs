using Entities.Entities.CommonField;
using Entities.Entities.Security;
using System;

namespace Entities.Entities
{
    public class UserProduct : Id_Field
    {
        public long ProductId { get; set; }
        public long UserId { get; set; }
        public double Price { get; set; }
        public string SpotPlayerToken { get; set; }
        public string SpotPlayerUrl { get; set; }
        public string SpotPlayerId { get; set; }
        public DateTime CreateDate { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
