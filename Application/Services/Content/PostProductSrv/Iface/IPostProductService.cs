using Application.Common.Dto.Result;
using Application.Services.Content.PostProductSrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.Content.PostProductSrv.Iface
{
    public interface IPostProductService
    {
        Task<BaseResultDto<PostProductDto>> GetPostProductsAsync(long postId);
        void InsertOrUpdate(Post post, long productId);
        BaseResultDto InsertOrUpdate(PostProductDto postProduct);
    }
}
