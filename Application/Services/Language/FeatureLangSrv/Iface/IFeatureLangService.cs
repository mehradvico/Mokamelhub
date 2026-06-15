using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.FeatureLangSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Language.FeatureLangSrv.Iface
{
    public interface IFeatureLangService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateAsyncDto(FeatureLangDto dto);
        BaseResultDto DeleteDto(OtherLangDeleteDto dto);
        FeatureLangSearchDto SearchDto(FeatureLangInputDto dto);
    }
}
