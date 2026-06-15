using Application.Common.Dto.Result;
using Application.Common.Service;
using Application.Services.Content.StaticPageSrv.Dto;
using Application.Services.Content.StaticPageSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Content.StaticPageSrv
{
    public class StaticPageService : CommonSrv<StaticPage, StaticPageDto>, IStaticPageService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;

        public StaticPageService(IDataBaseContext _context, IMapper mapper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }

        public async Task<BaseResultDto> GetByLabelAsync(string label)
        {
            var item = await _context.StaticPages.FirstOrDefaultAsync(s => s.Label == label);
            if (item != null)
                return new BaseResultDto<StaticPageVDto>(true, mapper.Map<StaticPageVDto>(item));
            return new BaseResultDto(false, val: Resource.Notification.ResourceNotFind);
        }

        public BaseSearchDto<StaticPageVDto> Search(StaticPageInputDto searchDto)
        {
            var query = _context.StaticPages.AsQueryable();


            if (!string.IsNullOrEmpty(searchDto.Label))
            {
                query = query.Where(s => s.Label.Contains(searchDto.Label));
            }
            if (!string.IsNullOrEmpty(searchDto.Q))
            {
                query = query.Where(s => s.Name.Contains(searchDto.Q));
            }

            return new BaseSearchDto<StaticPage, StaticPageVDto>(searchDto, query, mapper);
        }


    }
}
