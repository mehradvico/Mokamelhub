using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.ProductSrvs.DiscountGroupSrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.DiscountGroupSrv.Iface
{
    public interface IDiscountGroupService : ICommonSrv<DiscountGroup, DiscountGroupDto>
    {
        BaseSearchDto<DiscountGroupVDto> Search(BaseInputDto baseSearchDto);
        Task<BaseResultDto<DiscountGroupVDto>> FindAsyncVDto(long id);
    }
}
