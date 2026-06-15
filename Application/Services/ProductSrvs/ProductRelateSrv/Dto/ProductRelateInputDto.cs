using Application.Common.Dto.Input;
using Application.Services.ProductSrvs.ProductRelateSrv.Iface;

namespace Application.Services.ProductSrvs.ProductRelateSrv.Dto
{
    public class ProductRelateInputDto : BaseInputDto, IProductRelateSearchFields
    {
        public string Label { get; set; }

        public long? ProductId { get; set; }
    }
}
