using Application.Common.Dto.Input;
using Application.Services.CommonSrv.CommentLikeSrv.Iface;

namespace Application.Services.CommonSrv.CommentLikeSrv.Dto
{
    public class CommentLikeInputDto : BaseInputDto, ICommentLikeSearchFields
    {
        public long CommentId { get; set; }
    }
}
