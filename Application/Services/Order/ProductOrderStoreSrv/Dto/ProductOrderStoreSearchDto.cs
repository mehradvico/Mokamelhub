using Application.Common.Dto.Result;
using Application.Services.Order.ProductOrderStoreSrv.Dto;
using Application.Services.Order.ProductOrderStoreSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Order.ProductOrderStoreOrderSrv.Dto
{
    public class ProductOrderStoreSearchDto : BaseSearchDto<ProductOrderStore, ProductOrderStoreVDto>, IProductOrderStoreSearchFields
    {
        public ProductOrderStoreSearchDto(ProductOrderStoreInputDto dto, IQueryable<ProductOrderStore> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.ProductOrderId = dto.ProductOrderId;
            this.StoreId = dto.StoreId;
            this.UserId = dto.UserId;

        }
        public string ProductOrderId { get; set; }
        public long? StoreId { get; set; }
        public long? UserId { get; set; }



    }
}
