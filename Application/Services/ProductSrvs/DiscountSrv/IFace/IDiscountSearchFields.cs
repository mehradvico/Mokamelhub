using Application.Common.Enumerable;

namespace Application.Services.ProductSrvs.DiscountSrv.Iface
{
    public interface IDiscountSearchFields
    {
        public long? StoreId { get; set; }
        public long? CategoryId { get; set; }
        public long? BrandId { get; set; }
        public long? ProductId { get; set; }
        public long? ProductItemId { get; set; }
        public long? DiscountGroupId { get; set; }

        public DiscountTypeEnum? DiscountType { get; set; }
    }
}
