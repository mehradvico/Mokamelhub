using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.ProductSrvs.FeatureSrv.Dto;
using Entities.Entities;

namespace Application.Services.ProductSrvs.FeatureSrv.Iface
{
    public interface IFeatureService : ICommonSrv<Feature, FeatureDto>
    {
        FeatureSearchDto Search(FeatureInputDto searchDto);
        BaseResultDto GetForCategory(FeatureInputDto searchDto);

    }
}
