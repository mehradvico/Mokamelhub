using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.ProductRelateSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.ProductSrvs.ProductRelateSrv.Dto
{
    public class ProductRelateSearchDto : BaseSearchDto<ProductRelate, ProductRelateVDto>, IProductRelateSearchFields
    {
        public ProductRelateSearchDto(ProductRelateInputDto dto, IQueryable<ProductRelate> list, IMapper mapper) : base(dto, list, mapper)

        {
            ProductId = dto.ProductId;
            Label = dto.Label;
        }
        public long? ProductId { get; set; }
        public string Label { get; set; }

    }
}
