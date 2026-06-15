using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Dto;
using Entities.Entities.Security;

namespace Application.Services.Accounting.RoleSrv.Iface
{
    public interface IRoleService : ICommonSrv<Role, RoleDto>
    {
        BaseSearchDto<RoleDto> Search(BaseInputDto baseSearchDto);
        BaseResultDto InsertRolePermission(long roleId, long permissionId);
        BaseResultDto DeleteRolePermission(long roleId, long permissionId);
    }
}
