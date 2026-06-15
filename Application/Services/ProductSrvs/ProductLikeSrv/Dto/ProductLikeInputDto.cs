using Application.Common.Dto.Input;
using Application.Services.ProductSrvs.ProductLikeSrv.Iface;

namespace Application.Services.ProductSrvs.ProductLikeSrv.Dto
{
    public class ProductLikeInputDto : BaseInputDto, IProductLikeSearchFields
    {
        public long UserId { get; set; }
    }
}
