using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.BrandLangSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Language.BrandLangSrv.Iface
{
    public interface IBrandLangService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateAsyncDto(BrandLangDto dto);
        BaseResultDto DeleteDto(OtherLangDeleteDto dto);
        BrandLangSearchDto SearchDto(BrandLangInputDto dto);
    }
}
