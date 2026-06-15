using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.ProductSrvs.ProductCommentSrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.ProductCommentSrv.Iface
{
    public interface IProductCommentService : ICommonSrv<ProductComment, ProductCommentDto>
    {
        Task<BaseResultDto> UpdateDtoAsync(ProductCommentDto dto);
        ProductCommentSearchDto Search(ProductCommentInputDto baseSearchDto);
        Task UpdateProductCommentRateAsync(long Id);

    }
}
