using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.StateLangSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Language.StateLangSrv.Iface
{
    public interface IStateLangService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateAsyncDto(StateLangDto dto);
        BaseResultDto DeleteDto(OtherLangDeleteDto dto);
        StateLangSearchDto SearchDto(StateLangInputDto dto);
    }
}
