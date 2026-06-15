using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.ProductLikeSrv.Iface;
using Application.Services.ProductSrvs.ProductSrv.Dto;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.ProductSrvs.ProductLikeSrv.Dto
{
    public class ProductLikeSearchDto : BaseSearchDto<Product, ProductVDto>, IProductLikeSearchFields
    {
        public ProductLikeSearchDto(ProductLikeInputDto dto, IQueryable<Product> list, IMapper mapper) : base(dto, list, mapper)
        {
            UserId = dto.UserId;
        }

        public long UserId { get; set; }
    }
}
