using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Setting.SettingSrv.Dto;
using Entities.Entities;

namespace Application.Services.Setting.SettingSrv
{
    public interface IAdminSettingService : ICommonSrv<AdminSetting, AdminSettingDto>
    {
        AdminSetting GetByLabel(string label);
        BaseSearchDto<AdminSettingDto> Search(BaseInputDto baseSearchDto);
    }
}
