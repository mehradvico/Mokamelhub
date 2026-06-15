using Application.Common.Dto.Result;
using Application.Services.Accounting.PermissionSrv.Iface;
using AutoMapper;
using Entities.Entities.Security;
using System.Linq;

namespace Application.Services.Accounting.PermissionSrv.Dto
{
    public class PermissionSearchDto : BaseSearchDto<Permission, PermissionVDto>, IPermissionSearchFields
    {
        public PermissionSearchDto(PermissionInputDto dto, IQueryable<Permission> list, IMapper mapper) : base(dto, list, mapper)
        {
            RoleId = dto.RoleId;
            ParentId = dto.ParentId;
        }

        public long? RoleId { get; set; }
        public long? ParentId { get; set; }

    }
}
