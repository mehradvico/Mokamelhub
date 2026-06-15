using Application.Common.Dto.Field;
using Application.Services.Dto;
using Application.Services.Order.ProductOrderSrv.Dto;
using Entities.Entities;
using System;

namespace Application.Services.ProductSrvs.WalletSrv.Dto
{
    public class WalletVDto : Name_FieldDto
    {
        public bool IsIncrease { get; set; }
        public double Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public long UserId { get; set; }
        public long? PaymentId { get; set; }
        public string ProductOrderId { get; set; }
        public bool Painding { get; set; }

        public UserVDto User { get; set; }
        public PaymentVDto Payment { get; set; }
        public ProductOrderVDto ProductOrder { get; set; }
    }
}
