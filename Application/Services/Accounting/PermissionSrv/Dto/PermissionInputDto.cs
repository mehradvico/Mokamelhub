using Application.Common.Dto.Input;
using Application.Services.Accounting.PermissionSrv.Iface;

namespace Application.Services.Accounting.PermissionSrv.Dto
{
    public class PermissionInputDto : BaseInputDto, IPermissionSearchFields
    {
        public long? RoleId { get; set; }
        public long? ParentId { get; set; }
    }
}
