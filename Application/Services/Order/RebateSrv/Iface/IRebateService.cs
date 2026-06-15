using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Order.RebateSrv.Dto;
using Entities.Entities;

namespace Application.Services.Order.RebateSrv.Iface
{
    public interface IRebateService : ICommonSrv<Rebate, RebateDto>
    {
        BaseSearchDto<RebateDto> Search(BaseInputDto baseSearchDto);
        BaseResultDto<RebateVDto> GetRebateByCodeAsync(Cart cart, string Code);
        void IncreaseUseCount(ProductOrder order);

    }
}
