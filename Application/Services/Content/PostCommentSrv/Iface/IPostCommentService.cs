using Application.Common.Interface;
using Application.Services.Content.PostCommentSrv.Dto;
using Entities.Entities;

namespace Application.Services.Content.PostCommentSrv.Iface
{
    public interface IPostCommentService : ICommonSrv<PostComment, PostCommentDto>
    {
        PostCommentSearchDto Search(PostCommentInputDto baseSearchDto);
    }
}
