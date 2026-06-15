using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.FeatureItemSrv.Dto;

namespace Application.Services.ProductSrvs.ProductFeatureValueSrv.Iface
{
    public interface IProductFeatureValueService
    {
        ProductFeatureValueSearchDto Search(ProductFeatureValueInputDto searchDto);
        BaseResultDto GetForProduct(long productId);
        BaseResultDto SetForProduct(ProductFeatureValueAddDto productFeatureValues);
    }
}
