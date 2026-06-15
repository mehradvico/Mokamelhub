using Application.Services.Accounting.PermissionSrv.Dto;
using Application.Services.Dto;
using System.Collections.Generic;

namespace Application.Services.Accounting.RolePermission.Dto
{
    public class RolePermissionDto
    {
        public RoleDto RoleDto { get; set; }
        public List<PermissionDto> PermissionsDto { get; set; }
    }
}
