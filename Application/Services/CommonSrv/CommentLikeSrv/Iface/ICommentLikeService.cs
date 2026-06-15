using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.CommonSrv.CommentLikeSrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.CommonSrv.CommentLikeSrv.Iface
{
    public interface ICommentLikeService : ICommonSrv<CommentLike, CommentLikeDto>
    {
        CommentLikeSearchDto SearchDto(CommentLikeInputDto dto);
        Task<BaseResultDto> InsertOrDeleteAsync(CommentLikeInsertDeleteDto dto);
    }
}
