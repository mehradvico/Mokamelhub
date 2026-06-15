using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.ProductCommentSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.ProductSrvs.ProductCommentSrv.Dto
{
    public class ProductCommentSearchDto : BaseSearchDto<ProductComment, ProductCommentVDto>, IProductCommentSearchFields
    {
        public ProductCommentSearchDto(ProductCommentInputDto dto, IQueryable<ProductComment> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.ProductId = dto.ProductId;
            this.UserId = dto.UserId;
            this.AllStatus = dto.AllStatus;

        }
        public long? ProductId { get; set; }
        public long? UserId { get; set; }
        public bool? AllStatus { get; set; }

    }
}
