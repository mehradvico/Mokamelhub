namespace Application.Services.CategorySrv.Iface
{
    public interface ICategorySearchFields
    {
        public bool? Active { get; set; }
        public long? ParentId { get; set; }

    }
}
