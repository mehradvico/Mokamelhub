using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.CategorySrv.Dto;
using Application.Services.CommonSrv.SearchSrv.Dto;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.CategorySrv.Iface
{
    public interface ICategoryService : ICommonSrv<Category, CategoryDto>
    {
        CategorySearchDto Search(CategoryInputDto baseSearchDto);
        Task<BaseResultDto<CategoryVDto>> FindVDtoAsync(long id, bool? active = null);
        Task<BaseResultDto<CategoryParentVDto>> FindParentVDtoAsync(long id, bool? active = null);
        Task<BaseResultDto<CategoryChildrenVDto>> FindChildrenVDtoAsync(long id, bool? active = null);
        Task<BaseResultDto<CategoryChildrenVDto>> FindChildrenVDtoAsync(string label, bool? active = null);
        Task<BaseResultDto<CategoryCompleteVDto>> FindCompleteVDtoAsync(long id, bool? active = null);
        Task<List<CategoryVDto>> GetAllParents(long id, bool? active = null);
        Category Find(long id, bool? active = null);
        List<long> GetAllParentIds(long id);
        List<long> GetAllParentIds(string label);
        BaseResultDto GetSiteMap(string label);
        List<long> GetAllChildrenIds(string label);
        List<long> GetAllChildrenIds(long id);
        Task<BaseResultDto<CategoryChildrenMinVDto>> GetTreeAsync(long parentId, string lang = null);
        Task<BaseResultDto<List<long>>> CategoryStore(long storeId);
        Task<List<SearchCategoryDto>> SearchMinAsync(SearchRequestDto request);
        Task<BaseResultDto<List<SearchCategoryDto>>> GetAllActiveParents(long categoryId);
    }
}
