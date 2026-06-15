using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.BrandCategorySrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.BrandCategorySrv.Iface
{
    public interface IBrandCategoryService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateAsyncDto(BrandCategoryDto dto);
    }
}
