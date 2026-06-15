using Application.Services.CommonSrv.CommentSrv.Dto;

namespace Application.Services.Content.PostCommentSrv.Dto
{
    public class PostCommentDto : CommentDto
    {
        public long PostId { get; set; }
    }
}
