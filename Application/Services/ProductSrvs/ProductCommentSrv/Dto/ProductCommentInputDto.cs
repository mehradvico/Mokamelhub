using Application.Common.Dto.Input;
using Application.Services.Dto;
using Application.Services.ProductSrvs.ProductCommentSrv.Iface;

namespace Application.Services.ProductSrvs.ProductCommentSrv.Dto
{
    public class ProductCommentInputDto : BaseInputDto, IProductCommentSearchFields
    {

        public long? ProductId { get; set; }
        public long? UserId { get; set; }
        public bool? AllStatus { get; set; }
    }
}
