using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.CommonSrv.StateSrv.Dto;
using Entities.Entities;

namespace Application.Services.CommonSrv.StateSrv.Iface
{
    public interface IStateService : ICommonSrv<State, StateDto>
    {
        BaseSearchDto<StateVDto> Search(StateInputDto baseSearchDto);
    }
}
