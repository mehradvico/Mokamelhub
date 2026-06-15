using Application.Common.Dto.Result;
using Application.Services.Order.ProductOrderItemSrv.Dto;
using Application.Services.Order.ProductOrderItemSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Order.ProductOrderItemOrderSrv.Dto
{
    public class ProductOrderItemSearchDto : BaseSearchDto<ProductOrderItem, ProductOrderItemVDto>, IProductOrderItemSearchFields
    {
        public ProductOrderItemSearchDto(ProductOrderItemInputDto dto, IQueryable<ProductOrderItem> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.ProductOrderStoreId = dto.ProductOrderStoreId;

        }
        public long ProductOrderStoreId { get; set; }

    }
}
