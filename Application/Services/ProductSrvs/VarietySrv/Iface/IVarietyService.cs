using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.ProductSrvs.VarietySrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.VarietySrv.Iface
{
    public interface IVarietyService : ICommonSrv<Variety, VarietyDto>
    {
        Task<BaseResultDto<VarietyVDto>> FindAsyncVDto(long id);

        BaseSearchDto<VarietyVDto> SearchDto(BaseInputDto dto);
    }
}
