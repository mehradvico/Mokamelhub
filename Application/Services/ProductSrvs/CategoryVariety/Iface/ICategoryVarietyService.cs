using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.CategoryVariety.Dto;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.CategoryVariety.Iface
{
    public interface ICategoryVarietyService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateAsyncDto(CategoryVarietyDto dto);
    }
}
