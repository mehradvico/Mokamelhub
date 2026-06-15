using Application.Services.CommonSrv.CommentSrv.Dto;

namespace Application.Services.ProductSrvs.ProductCommentSrv.Dto
{
    public class ProductCommentDto : CommentDto
    {
        public long ProductId { get; set; }
    }
}
