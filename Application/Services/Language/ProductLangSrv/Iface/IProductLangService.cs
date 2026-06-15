using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.ProductLangSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Language.ProductLangSrv.Iface
{
    public interface IProductLangService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateDto(ProductLangDto dto);
        BaseResultDto DeleteDto(OtherLangDeleteDto dto);
        ProductLangSearchDto searchDto(ProductLangInputDto dto);
    }
}
