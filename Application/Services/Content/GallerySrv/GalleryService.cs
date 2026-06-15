using Application.Common.Dto.Result;
using Application.Common.Service;
using Application.Services.Content.GallerySrv.Dto;
using Application.Services.Content.GallerySrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Content.GallerySrv
{
    public class GalleryService : CommonSrv<Gallery, GalleryDto>, IGalleryService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        private readonly IQueryable<Gallery> _baseQuery;

        public GalleryService(IDataBaseContext _context, IMapper mapper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
            _baseQuery = _context.Galleries.Where(s => s.Deleted == false);
        }
        public async Task<BaseResultDto<GalleryVDto>> FindVDtoAsync(string label)
        {
            var item = await _baseQuery.Include(s => s.Picture).FirstOrDefaultAsync(s => s.Label == label && s.Active);
            if (item != null)
            {
                return new BaseResultDto<GalleryVDto>(true, mapper.Map<GalleryVDto>(item));
            }
            return new BaseResultDto<GalleryVDto>(false, mapper.Map<GalleryVDto>(item));
        }
        public BaseSearchDto<GalleryVDto> Search(GalleryInputDto searchDto)
        {
            var query = _baseQuery.Include(s => s.Picture).Include(s => s.Category).AsQueryable();
            if (searchDto.CategoryId.HasValue)
            {
                query = query.Where(s => s.CategoryId == searchDto.CategoryId);
            }
            else if (!string.IsNullOrEmpty(searchDto.CategoryLabel))
            {
                query = query.Where(s => s.Category.Label == searchDto.CategoryLabel);
            }
            if (searchDto.Available.HasValue)
            {
                query = query.Where(s => s.Active == searchDto.Available);
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
                            query = query.OrderByDescending(s => s.Priority);

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
                            query = query.OrderByDescending(s => s.Priority);
                            break;
                        }
                    case Common.Enumerable.SortEnum.LessPriority:
                        {
                            query = query.OrderBy(s => s.Priority);
                            break;
                        }
                    default:
                        break;
                }
            }
            return new BaseSearchDto<Gallery, GalleryVDto>(searchDto, query, mapper);
        }
    }
}
