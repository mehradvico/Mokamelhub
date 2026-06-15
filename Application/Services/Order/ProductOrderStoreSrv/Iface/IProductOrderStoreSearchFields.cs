namespace Application.Services.Order.ProductOrderStoreSrv.Iface
{
    public interface IProductOrderStoreSearchFields
    {
        public string ProductOrderId { get; set; }
        public long? StoreId { get; set; }
        public long? UserId { get; set; }

    }
}
