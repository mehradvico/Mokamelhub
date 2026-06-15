namespace Application.Services.CommonSrv.CommentLikeSrv.Dto
{
    public class CommentLikeInsertDeleteDto
    {
        public bool? IsLike { get; set; }
        public long CommentId { get; set; }
    }
}
