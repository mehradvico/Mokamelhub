using Application.Common.Dto.Field;
using System;

namespace Application.Services.ProductSrvs.WalletSrv.Dto
{
    public class WalletDto : Id_FieldDto
    {
        public string Name { get; set; }
        public bool IsIncrease { get; set; }
        public double Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public long UserId { get; set; }
        public long? PaymentId { get; set; }
        public string ProductOrderId { get; set; }
        public bool Painding { get; set; }
    }
}
