using Entities.Entities.CommonField;
using Entities.Entities.Security;
using System;

namespace Entities.Entities
{
    public class Wallet : Name_Field
    {
        public bool IsIncrease { get; set; }
        public double Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public long UserId { get; set; }
        public long? PaymentId { get; set; }
        public string ProductOrderId { get; set; }
        public bool Painding { get; set; }
        public bool Deleted { get; set; }
        public User User { get; set; }
        public Payment Payment { get; set; }
        public ProductOrder ProductOrder { get; set; }
    }
}
