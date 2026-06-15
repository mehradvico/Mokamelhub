namespace Application.Services.Content.PostCommentSrv.Iface
{
    public interface IPostCommentSearchFields
    {
        public long? PostId { get; set; }
        public bool? AllStatus { get; set; }

    }
}
