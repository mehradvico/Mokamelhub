using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Setting.CodeGroupSrv.Dto;
using Entities.Entities;

namespace Application.Services.Setting.CodeGroupSrv.Iface
{
    public interface ICodeGroupService : ICommonSrv<CodeGroup, CodeGroupDto>
    {
        BaseSearchDto<CodeGroupDto> Search(BaseInputDto baseSearchDto);
    }
}
