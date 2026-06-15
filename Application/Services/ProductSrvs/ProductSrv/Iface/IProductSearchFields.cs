using Application.Common.Dto.LocationPoint;
using Application.Common.Enumerable;

namespace Application.Services.ProductSrv.Iface
{
    public interface IProductSearchFields
    {
        public long[] FeatureItemIds { get; set; }

        public long[] CategoryIds { get; set; }
        public string[] CategoryLabels { get; set; }
        public bool IsAndCategories { get; set; }
        public bool? Active { get; set; }
        public long? NotId { get; set; }
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }
        public bool? InStock { get; set; }
        public long[] BrandIds { get; set; }
        public long? StoreId { get; set; }
        public long? CreateStoreId { get; set; }
        public string DiscountGroupLabel { get; set; }
        public bool? HasDiscount { get; set; }
        public bool IsAdmin { get; set; }
        public ProductStatusEnum? Status { get; set; }
        public ProductTypeEnum? Type { get; set; }
        public PointDto Distance { get; set; }


    }
}
