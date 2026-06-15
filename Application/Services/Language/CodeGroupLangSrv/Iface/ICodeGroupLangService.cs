using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.CodeGroupLangSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Language.CodeGroupLangSrv.Iface
{
    public interface ICodeGroupLangService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateAsyncDto(CodeGroupLangDto dto);
        BaseResultDto DeleteDto(OtherLangDeleteDto dto);
        CodeGroupLangSearchDto SearchDto(CodeGroupLangInputDto dto);
    }
}
