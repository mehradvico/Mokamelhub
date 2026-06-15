using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.CategoryLangSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Language.CategoryLangSrv.Iface
{
    public interface ICategoryLangService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateAsyncDto(CategoryLangDto dto);
        BaseResultDto DeleteDto(OtherLangDeleteDto dto);
        CategoryLangSearchDto SearchDto(CategoryLangInputDto dto);
    }
}
