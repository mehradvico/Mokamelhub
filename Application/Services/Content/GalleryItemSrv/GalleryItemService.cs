using Application.Common.Dto.Result;
using Application.Common.Service;
using Application.Services.Content.GalleryItemSrv.Dto;
using Application.Services.Content.GalleryItemSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Linq;

namespace Application.Services.Content.GalleryItemSrv
{
    public class GalleryItemService : CommonSrv<GalleryItem, GalleryItemDto>, IGalleryItemService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public GalleryItemService(IDataBaseContext _context, IMapper mapper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }
        public BaseSearchDto<GalleryItemDto> Search(GalleryItemInputDto searchDto)
        {
            var query = _context.GalleryItems.Include(s => s.Picture).Where(s => s.GalleryId == searchDto.GalleryId).AsQueryable();
            if (!string.IsNullOrEmpty(searchDto.Q))
            {
                query = query.Where(s => s.Name.Contains(searchDto.Q));
            }
            if (searchDto.Available.HasValue)
            {
                query = query.Where(s => s.Active == searchDto.Available.Value);
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

            return new BaseSearchDto<GalleryItem, GalleryItemDto>(searchDto, query, mapper);
        }
    }
}
