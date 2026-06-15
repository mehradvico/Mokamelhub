using Application.Common.Interface;
using Application.Services.ProductSrvs.ProductLikeSrv.Dto;
using Entities.Entities;

namespace Application.Services.ProductSrvs.ProductLikeSrv.Iface
{
    public interface IProductLikeService : ICommonSrv<ProductLike, ProductLikeDto>
    {
        ProductLikeSearchDto SearchDto(ProductLikeInputDto dto);
    }
}
