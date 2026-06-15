using Application.Services.CommonSrv.CommentSrv.Dto;

namespace Application.Services.ProductSrvs.StoreCommentSrv.Dto
{
    public class StoreCommentVDto : CommentVDto
    {
        public long StoreId { get; set; }
        public string StoreName { get; set; }
    }
}
