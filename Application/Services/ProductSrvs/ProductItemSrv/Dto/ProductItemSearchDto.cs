using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.ProductItemSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.ProductSrvs.ProductItemSrv.Dto
{
    public class ProductItemSearchDto : BaseSearchDto<ProductItem, ProductItemVDto>, IProductItemSearchFields
    {
        public ProductItemSearchDto(ProductItemInputDto dto, IQueryable<ProductItem> list, IMapper mapper) : base(dto, list, mapper)

        {
            ProductId = dto.ProductId;
        }
        public long? ProductId { get; set; }
    }
}
