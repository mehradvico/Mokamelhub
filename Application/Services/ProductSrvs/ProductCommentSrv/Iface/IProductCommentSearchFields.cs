namespace Application.Services.ProductSrvs.ProductCommentSrv.Iface
{
    public interface IProductCommentSearchFields
    {
        public long? ProductId { get; set; }
        public long? UserId { get; set; }
        public bool? AllStatus { get; set; }

    }
}
