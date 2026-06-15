using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.PostLangSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Language.PostLangSrv.Iface
{
    public interface IPostLangService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateAsyncDto(PostLangDto dto);
        BaseResultDto DeleteDto(OtherLangDeleteDto dto);
        PostLangSearchDto SearchDto(PostLangInputDto searchDto);
    }
}
