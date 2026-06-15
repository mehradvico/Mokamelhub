using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.VarietyLangSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Language.VarietyLangSrv.Iface
{
    public interface IVarietyLangService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateAsyncDto(VarietyLangDto dto);
        BaseResultDto DeleteDto(OtherLangDeleteDto dto);
        VarietyLangSearchDto SearchDto(VarietyLangInputDto dto);
    }
}
