using Application.Common.Enumerable;

namespace Application.Services.Accounting.UserSrv.Iface
{
    public interface IUserSearchFields
    {
        public long? RoleId { get; set; }
        public RoleEnum? RoleEnum { get; set; }
    }
}
