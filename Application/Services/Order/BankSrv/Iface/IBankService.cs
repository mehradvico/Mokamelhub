using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Order.BankSrv.Dto;
using Entities.Entities;

namespace Application.Services.Order.BankSrv.Iface
{
    public interface IBankService : ICommonSrv<Bank, BankDto>
    {
        BaseSearchDto<BankVDto> Search(BaseInputDto baseSearchDto);

    }
}

