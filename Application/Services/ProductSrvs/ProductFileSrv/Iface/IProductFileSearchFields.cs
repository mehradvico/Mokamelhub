namespace Application.Services.ProductSrvs.ProductFileSrv.Iface
{
    public interface IProductFileSearchFields
    {
        public long? ProductId { get; set; }
        public long? ParentId { get; set; }
        public long? UserId { get; set; }

    }
}
