using Application.Common.Dto.Input;
using Application.Services.ProductSrvs.WalletSrv.IFace;
using System;

namespace Application.Services.ProductSrvs.WalletSrv.Dto
{
    public class WalletInputDto : BaseInputDto, IWalletSearchFields
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public long? UserId { get; set; }
        public bool? IsIncrease { get; set; }

    }
}
