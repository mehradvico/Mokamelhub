using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.WalletSrv.IFace;
using AutoMapper;
using Entities.Entities;
using System;
using System.Linq;

namespace Application.Services.ProductSrvs.WalletSrv.Dto
{
    public class WalletSearchDto : BaseSearchDto<Wallet, WalletVDto>, IWalletSearchFields
    {
        public WalletSearchDto(WalletInputDto dto, IQueryable<Wallet> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.DateFrom = dto.DateFrom;
            this.DateTo = dto.DateTo;
            this.UserId = dto.UserId;
            this.IsIncrease = dto.IsIncrease;
        }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public long? UserId { get; set; }
        public bool? IsIncrease { get; set; }


    }
}
