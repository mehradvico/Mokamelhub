namespace Application.Services.Accounting.PermissionSrv.Iface
{
    public interface IPermissionSearchFields
    {
        public long? RoleId { get; set; }
        public long? ParentId { get; set; }

    }
}
