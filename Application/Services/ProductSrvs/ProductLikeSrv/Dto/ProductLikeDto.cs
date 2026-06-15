using Application.Common.Dto.Field;

namespace Application.Services.ProductSrvs.ProductLikeSrv.Dto
{
    public class ProductLikeDto : Id_FieldDto
    {
        public long ProductId { get; set; }
        public long UserId { get; set; }

    }
}
