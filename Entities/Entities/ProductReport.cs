using Entities.Entities.CommonField;
using Entities.Entities.Security;
using System;

namespace Entities.Entities
{
    public class ProductReport : Id_Field
    {
        public long ProductId { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ReportDetail { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
}
