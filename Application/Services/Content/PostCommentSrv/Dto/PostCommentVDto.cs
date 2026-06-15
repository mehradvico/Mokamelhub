using Application.Services.CommonSrv.CommentSrv.Dto;

namespace Application.Services.Content.PostCommentSrv.Dto
{
    public class PostCommentVDto : CommentVDto
    {
        public long PostId { get; set; }
        public string PostName { get; set; }
    }
}
