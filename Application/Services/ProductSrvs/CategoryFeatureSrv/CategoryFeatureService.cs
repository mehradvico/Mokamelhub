using Application.Common.Dto.Result;
using Application.Services.CategorySrv.Iface;
using Application.Services.ProductSrvs.FeatureItemSrv.Dto;
using Application.Services.ProductSrvs.ProductFeatureValueSrv.Iface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.ProductFeatureValueSrv
{
    public class CategoryFeatureService : ICategoryFeatureService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        private readonly ICategoryService _categoryService;
        public CategoryFeatureService(IDataBaseContext context, IMapper mapper, ICategoryService categoryService)
        {
            this._context = context;
            this.mapper = mapper;
            this._categoryService = categoryService;
        }

        public BaseResultDto GetForCategory(long CategoryId)
        {
            var categorIds = _categoryService.GetAllParentIds(CategoryId);
            var categories = _context.Categories.Include(s => s.Features).ThenInclude(s => s.FeatureItems).Where(s => categorIds.Any(a => s.Id.Equals(a)));
            return new BaseResultDto<List<CategoryFeatureDto>>(isSuccess: true, data: mapper.Map<List<CategoryFeatureDto>>(categories));
        }
        public async Task<BaseResultDto> UpdateAsync(CategoryFeatureDto categoryFeature)
        {
            var category = await _context.Categories.Include(s => s.Features).AsTracking().FirstOrDefaultAsync(s => s.Id == categoryFeature.Id);
            category.Features.Clear();
            var featureList = _context.Features.AsTracking().Where(s => categoryFeature.Features.Select(f => f.Id).Contains(s.Id));
            foreach (var feature in featureList)
            {
                category.Features.Add(feature);
            }
            _context.SaveChanges();
            return new BaseResultDto(isSuccess: true);
        }

    }
}
