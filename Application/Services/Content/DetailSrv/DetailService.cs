using Application.Common.Dto.Result;
using Application.Common.Service;
using Application.Services.Content.DetailSrv.Dto;
using Application.Services.Content.DetailSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Content.DetailSrv
{
    public class DetailService : CommonSrv<Detail, DetailDto>, IDetailService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;

        public DetailService(IDataBaseContext _context, IMapper mapper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;

        }

        public async Task<BaseResultDto<DetailVDto>> GetByLabelAsync(string label)
        {
            var item = await _context.Details.FirstOrDefaultAsync(s => s.Label == label);
            if (item != null)
                return new BaseResultDto<DetailVDto>(true, mapper.Map<DetailVDto>(item));
            return new BaseResultDto<DetailVDto>(false, mapper.Map<DetailVDto>(item));
        }

        public BaseSearchDto<DetailVDto> Search(DetailInputDto searchDto)
        {
            var query = _context.Details.Include(s => s.Type).Include(s => s.Category).AsQueryable();

            if (searchDto.CategoryId.HasValue)
            {
                query = query.Where(s => s.CategoryId == searchDto.CategoryId);
            }
            else if (!string.IsNullOrEmpty(searchDto.CategoryLabel))
            {
                query = query.Where(s => s.Category.Label == searchDto.CategoryLabel);
            }

            if (!string.IsNullOrEmpty(searchDto.Q))
            {
                query = query.Where(s => s.Title.Contains(searchDto.Q));
            }
            if (searchDto.SortBy != Common.Enumerable.SortEnum.Default)
            {
                switch (searchDto.SortBy)
                {
                    case Common.Enumerable.SortEnum.Default:
                        {
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
                            query = query.OrderByDescending(s => s.Title);
                            break;
                        }

                    default:
                        break;
                }
            }
            return new BaseSearchDto<Detail, DetailVDto>(searchDto, query, mapper);
        }


    }
}
