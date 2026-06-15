using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Setting.BaseDetailSrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.Setting.BaseDetailSrv.Iface
{
    public interface IBaseDetailService : ICommonSrv<BaseDetail, BaseDetailDto>
    {
        BaseSearchDto<BaseDetailDto> Search(BaseInputDto baseSearchDto);
        BaseDetailDto GetBaseDetails();
        Task<BaseResultDto> GetDtoAsync(string label);
    }
}
