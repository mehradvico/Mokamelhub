namespace Application.Services.ProductSrvs.StoreCommentSrv.Iface
{
    public interface IStoreCommentSearchFields
    {
        public long? StoreId { get; set; }
        public bool? AllStatus { get; set; }
    }
}
