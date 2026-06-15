using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.FeatureItemLangSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Language.FeatureItemLangSrv.Iface
{
    public interface IFeatureItemLangService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateAsyncDto(FeatureItemLangDto dto);
        BaseResultDto DeleteDto(OtherLangDeleteDto dto);
        FeatureItemLangSearchDto SearchDto(FeatureItemLangInputDto dto);
    }
}
