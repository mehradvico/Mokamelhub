using Application.Services.CommonSrv.CommentSrv.Dto;

namespace Application.Services.ProductSrvs.ProductCommentSrv.Dto
{
    public class ProductCommentVDto : CommentVDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public bool? UserIsLike { get; set; }

    }
}
