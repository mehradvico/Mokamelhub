using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Service;
using Application.Services.Accounting.RoleSrv.Iface;
using Application.Services.Dto;
using AutoMapper;
using Entities.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Linq;

namespace Application.Services.RoleSrv
{
    public class RoleService : CommonSrv<Role, RoleDto>, IRoleService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;

        public RoleService(IDataBaseContext _context, IMapper mapper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }
        public BaseResultDto InsertRolePermission(long roleId, long permissionId)
        {
            var role = _context.Roles.Find(roleId);
            var permission = _context.Permissions.AsTracking().Include(s => s.Roles).FirstOrDefault(s => s.Id == permissionId);
            if (role != null && permission != null)
            {
                if (permission.Roles.Any(s => s.Id == roleId))
                {
                    return new BaseResultDto(false, val1: Resource.Notification.DuplicateValue, val2: nameof(permissionId));
                }
                permission.Roles.Add(role);
                _context.SaveChanges();
                return new BaseResultDto(true);
            }
            else
            {
                return new BaseResultDto(false, val: Resource.Notification.InvalidData);
            }
        }
        public BaseResultDto DeleteRolePermission(long roleId, long permissionId)
        {
            var role = _context.Roles.Find(roleId);
            var permission = _context.Permissions.Include(s => s.Roles).FirstOrDefault(s => s.Id == permissionId);
            if (role != null && permission != null)
            {
                permission.Roles.Remove(role);
                _context.SaveChanges();
                return new BaseResultDto(true);
            }
            else
            {
                return new BaseResultDto(false, val: Resource.Notification.InvalidData);
            }
        }

        public BaseSearchDto<RoleDto> Search(BaseInputDto baseSearchDto)
        {
            var model = _context.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(baseSearchDto.Q))
            {
                model = model.Where(s => s.Name.Contains(baseSearchDto.Q));
            }
            return new BaseSearchDto<Role, RoleDto>(baseSearchDto, model, mapper);
        }
    }
}
