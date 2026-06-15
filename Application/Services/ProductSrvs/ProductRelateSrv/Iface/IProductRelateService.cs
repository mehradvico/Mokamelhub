using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.ProductRelateSrv.Dto;

namespace Application.Services.ProductSrvs.ProductRelateSrv.Iface
{
    public interface IProductRelateService
    {
        BaseResultDto InsertOrUpdate(ProductRelateVDto productRelate);
        BaseResultDto<ProductRelateVDto> GetForProduct(ProductRelateDto dto);
    }
}
