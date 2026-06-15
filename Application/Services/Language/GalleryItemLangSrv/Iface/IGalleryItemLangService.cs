using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.GalleryItemLangSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Language.GalleryItemLangSrv.Iface
{
    public interface IGalleryItemLangService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateAsyncDto(GalleryItemLangDto dto);
        BaseResultDto DeleteDto(OtherLangDeleteDto dto);
        GalleryItemLangSearchDto SearchDto(GalleryItemLangInputDto searchDto);
    }
}
