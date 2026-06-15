using Application.Common.Dto.Input;
using Application.Services.ProductSrvs.ProductItemSrv.Iface;

namespace Application.Services.ProductSrvs.ProductItemSrv.Dto
{
    public class ProductItemInputDto : BaseInputDto, IProductItemSearchFields
    {
        public long? ProductId { get; set; }
        public long? StoreId { get; set; }
    }
}
