using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.GalleryLangSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Language.GalleryLangSrv.Iface
{
    public interface IGalleryLangService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateAsyncDto(GalleryLangDto dto);
        BaseResultDto DeleteDto(OtherLangDeleteDto dto);
        GalleryLangSearchDto SearchDto(GalleryLangInputDto searchDto);
    }
}
