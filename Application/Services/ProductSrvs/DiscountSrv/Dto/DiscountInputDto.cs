using Application.Common.Dto.Input;
using Application.Common.Enumerable;
using Application.Services.ProductSrvs.DiscountSrv.Iface;

namespace Application.Services.ProductSrvs.DiscountSrv.Dto
{
    public class DiscountInputDto : BaseInputDto, IDiscountSearchFields
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
