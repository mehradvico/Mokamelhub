using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.FeatureItemSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.ProductFeatureValueSrv.Iface
{
    public interface ICategoryFeatureService
    {
        BaseResultDto GetForCategory(long CategoryId);
        Task<BaseResultDto> UpdateAsync(CategoryFeatureDto categoryFeature);

    }
}
