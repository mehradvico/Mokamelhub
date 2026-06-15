using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.CodeLangSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Language.CodeLangSrv.Iface
{
    public interface ICodeLangService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateDto(CodeLangDto dto);
        BaseResultDto DeleteDto(OtherLangDeleteDto dto);
        CodeLangSearchDto SearchDto(CodeLangInputDto dto);
    }
}
