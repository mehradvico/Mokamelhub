using Application.Services.CommonSrv.CommentSrv.Dto;

namespace Application.Services.ProductSrvs.StoreCommentSrv.Dto
{
    public class StoreCommentDto : CommentDto
    {
        public long StoreId { get; set; }
    }
}
