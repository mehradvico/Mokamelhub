using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.ProductSrvs.DiscountSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;


namespace Application.Services.ProductSrvs.DiscountSrv.Dto
{
    public class DiscountSearchDto : BaseSearchDto<Discount, DiscountVDto>, IDiscountSearchFields
    {
        public DiscountSearchDto(DiscountInputDto dto, IQueryable<Discount> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.StoreId = dto.StoreId;
            this.CategoryId = dto.CategoryId;
            this.BrandId = dto.BrandId;
            this.ProductId = dto.ProductId;
            this.ProductItemId = dto.ProductItemId;
            this.DiscountType = dto.DiscountType;
            this.DiscountGroupId = dto.DiscountGroupId;
        }

        public long? StoreId { get; set; }
        public long? CategoryId { get; set; }
        public long? BrandId { get; set; }
        public long? ProductId { get; set; }
        public long? ProductItemId { get; set; }
        public long? DiscountGroupId { get; set; }

        public DiscountTypeEnum? DiscountType { get; set; }

    }
}
