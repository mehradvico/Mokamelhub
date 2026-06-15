using System;

namespace Application.Services.ProductSrvs.WalletSrv.IFace
{
    public interface IWalletSearchFields
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public long? UserId { get; set; }
        public bool? IsIncrease { get; set; }
    }
}
