using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.CityLangSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Language.CityLangSrv.Iface
{
    public interface ICityLangService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateAsyncDto(CityLangDto dto);
        BaseResultDto DeleteDto(OtherLangDeleteDto dto);
        CityLangSearchDto SearchDto(CityLangInputDto dto);
    }
}
