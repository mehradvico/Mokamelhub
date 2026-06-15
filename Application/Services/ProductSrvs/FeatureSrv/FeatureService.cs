using Application.Common.Dto.Result;
using Application.Common.Service;
using Application.Services.CategorySrv.Iface;
using Application.Services.ProductSrvs.FeatureSrv.Dto;
using Application.Services.ProductSrvs.FeatureSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.ProductSrvs.FeatureSrv
{
    public class FeatureService : CommonSrv<Feature, FeatureDto>, IFeatureService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        private readonly IQueryable<Feature> _baseQuery;
        private readonly ICategoryService _categoryService;

        public FeatureService(IDataBaseContext _context, IMapper mapper, ICategoryService categoryService) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
            _categoryService = categoryService;
            _baseQuery = _context.Features.Where(s => s.Deleted == false).AsQueryable();
        }
        public FeatureSearchDto Search(FeatureInputDto searchDto)
        {
            var query = _baseQuery.Include(s => s.FeatureItems).AsQueryable();
            if (searchDto.CategoryId.HasValue)
            {
                query = query.Where(s => s.Categories.Any(a => a.Id.Equals(searchDto.CategoryId)));
            }
            else if (!string.IsNullOrEmpty(searchDto.CategoryLabel))
            {
                query = query.Where(s => s.Categories.Any(a => a.Label.Equals(searchDto.CategoryLabel)));
            }
            if (searchDto.Available.HasValue)
            {
                query = query.Where(s => s.Active == searchDto.Available);
            }
            if (searchDto.IsGroup.HasValue)
            {
                query = query.Where(s => s.IsGroup == searchDto.IsGroup);
            }
            if (searchDto.IsHide.HasValue)
            {
                query = query.Where(s => s.Hide == searchDto.IsHide);
            }
            if (searchDto.InSearch.HasValue)
            {
                query = query.Where(s => s.InSearch == searchDto.InSearch);
            }
            if (searchDto.FeatureTypeEnum.HasValue)
            {
                query = query.Where(s => s.Type.Label == searchDto.FeatureTypeEnum.ToString());
            }
            if (!string.IsNullOrEmpty(searchDto.Q))
            {
                query = query.Where(s => s.Name.Contains(searchDto.Q));
            }
            if (searchDto.SortBy != Common.Enumerable.SortEnum.Default)
            {
                switch (searchDto.SortBy)
                {
                    case Common.Enumerable.SortEnum.Default:
                        {
                            query = query.OrderBy(s => s.Priority);

                            break;
                        }
                    case Common.Enumerable.SortEnum.New:
                        {
                            query = query.OrderByDescending(s => s.Id);
                            break;
                        }
                    case Common.Enumerable.SortEnum.Old:
                        {
                            query = query.OrderBy(s => s.Id);
                            break;
                        }
                    case Common.Enumerable.SortEnum.Name:
                        {
                            query = query.OrderByDescending(s => s.Name);
                            break;
                        }

                    case Common.Enumerable.SortEnum.MorePriority:
                        {
                            query = query.OrderBy(s => s.Priority);
                            break;
                        }
                    case Common.Enumerable.SortEnum.LessPriority:
                        {
                            query = query.OrderByDescending(s => s.Priority);
                            break;
                        }
                    default:
                        break;
                }
            }
            return new FeatureSearchDto(searchDto, query, mapper);
        }
        public BaseResultDto GetForCategory(FeatureInputDto searchDto)
        {
            var categoryIds = new List<long>();
            if (searchDto.CategoryId.HasValue)
            {
                if (searchDto.GetChildren)
                    categoryIds = _categoryService.GetAllChildrenIds(searchDto.CategoryId.Value);
                else
                    categoryIds = _categoryService.GetAllParentIds(searchDto.CategoryId.Value);
            }
            if (!string.IsNullOrEmpty(searchDto.CategoryLabel))
            {
                if (searchDto.GetChildren)
                    categoryIds = _categoryService.GetAllChildrenIds(searchDto.CategoryLabel);
                else
                    categoryIds = _categoryService.GetAllParentIds(searchDto.CategoryLabel);

            }
            var query = _context.Features.Where(s => s.Categories.Any(a => categoryIds.Contains(a.Id))).Include(s => s.FeatureItems).AsQueryable();
            if (searchDto.InSearch.HasValue)
            {
                query = query.Where(s => s.InSearch == searchDto.InSearch);
            }
            if (searchDto.IsGroup.HasValue)
            {
                query = query.Where(s => s.IsGroup == searchDto.IsGroup);
            }
            if (searchDto.FeatureTypeEnum.HasValue)
            {
                query = query.Where(s => s.Type.Label == searchDto.FeatureTypeEnum.ToString());
            }
            query = query.OrderBy(s => s.Priority).AsQueryable();

            return new BaseResultDto<List<FeatureVDto>>(isSuccess: true, mapper.Map<List<FeatureVDto>>(query));
        }

    }
}
