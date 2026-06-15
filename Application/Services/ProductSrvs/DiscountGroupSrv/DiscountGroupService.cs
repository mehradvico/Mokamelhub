using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Service;
using Application.Services.CategorySrv.Iface;
using Application.Services.ProductSrvs.DiscountGroupSrv.Dto;
using Application.Services.ProductSrvs.DiscountGroupSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.DiscountGroupSrv
{
    public class DiscountGroupService : CommonSrv<DiscountGroup, DiscountGroupDto>, IDiscountGroupService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        private readonly ICategoryService _categoryService;

        public DiscountGroupService(IDataBaseContext _context, IMapper mapper, ICategoryService categoryService) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
            this._categoryService = categoryService;
        }

        public BaseSearchDto<DiscountGroupVDto> Search(BaseInputDto baseSearchDto)
        {
            var model = _context.DiscountGroups.AsQueryable();
            if (baseSearchDto.Available.HasValue)
            {
                model = model.Where(s => s.Active == baseSearchDto.Available.Value);
            }
            if (!string.IsNullOrEmpty(baseSearchDto.Q))
            {
                model = model.Where(s => s.Name.Contains(baseSearchDto.Q));
            }
            switch (baseSearchDto.SortBy)
            {

                case Common.Enumerable.SortEnum.Default:
                    {
                        model = model.OrderByDescending(s => s.Id);
                        break;
                    }
                case Common.Enumerable.SortEnum.New:
                    {
                        model = model.OrderByDescending(s => s.Id);
                        break;
                    }
                case Common.Enumerable.SortEnum.Old:
                    {
                        model = model.OrderBy(s => s.Id);
                        break;
                    }
                case Common.Enumerable.SortEnum.Name:
                    {
                        model = model.OrderByDescending(s => s.Name);
                        break;
                    }
                default:
                    break;
            }

            return new BaseSearchDto<DiscountGroup, DiscountGroupVDto>(baseSearchDto, model, mapper);
        }
        public async Task<BaseResultDto<DiscountGroupVDto>> FindAsyncVDto(long id)
        {
            var item = await _context.DiscountGroups.Include(s => s.Picture).FirstOrDefaultAsync(s => s.Id == id && s.Active);
            if (item != null)
            {
                return new BaseResultDto<DiscountGroupVDto>(true, mapper.Map<DiscountGroupVDto>(item));
            }
            return new BaseResultDto<DiscountGroupVDto>(false, mapper.Map<DiscountGroupVDto>(item));
        }


    }
}
