using Application.Common.Dto.Input;
using Application.Services.ProductSrvs.ProductPictureSrv.Iface;

namespace Application.Services.ProductSrvs.ProductPictureSrv.Dto
{
    public class ProductPictureInputDto : BaseInputDto, IProductPictureSearchFields
    {
        public long? ProductId { get; set; }
    }
}
