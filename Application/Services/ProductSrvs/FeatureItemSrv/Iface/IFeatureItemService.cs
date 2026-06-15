using Application.Common.Interface;
using Application.Services.CommonSrv.SearchSrv.Dto;
using Application.Services.ProductSrvs.FeatureItemSrv.Dto;
using Application.Services.ProductSrvs.FeatureSrv.Dto;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.FeatureSrv.Iface
{
    public interface IFeatureItemService : ICommonSrv<FeatureItem, FeatureItemDto>
    {
        FeatureItemSearchDto Search(FeatureItemInputDto searchDto);
        Task<List<SearchFeatureItemDto>> SearchMinAsync(SearchRequestDto request);
    }
}
