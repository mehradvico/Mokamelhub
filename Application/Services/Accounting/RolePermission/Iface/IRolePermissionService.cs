using Application.Common.Dto.Result;
using Application.Services.Accounting.RolePermission.Dto;
using System.Threading.Tasks;

namespace Application.Services.Accounting.RolePermission.Iface
{
    public interface IRolePermissionService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateAsyncDto(RolePermissionDto dto);
    }
}
