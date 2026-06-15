using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Service;
using Application.Services.Content.NewsletterSrv.Dto;
using Application.Services.Content.NewsletterSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Content.NewsletterSrv
{
    public class NewsletterService : CommonSrv<Newsletter, NewsletterDto>, INewsletterService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;

        public NewsletterService(IDataBaseContext _context, IMapper mapper) : base(_context: _context, mapper: mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }
        public override Task<BaseResultDto<NewsletterDto>> InsertAsyncDto(NewsletterDto dto)
        {
            dto.CreateDate = DateTime.Now;
            return base.InsertAsyncDto(dto);
        }
        public BaseSearchDto<NewsletterDto> Search(BaseInputDto baseSearchDto)
        {
            var model = _context.Newsletters.AsQueryable();
            if (!string.IsNullOrEmpty(baseSearchDto.Q))
            {
                model = model.Where(s => s.Email.Contains(baseSearchDto.Q)).OrderByDescending(o => o.Id);
            }
            return new BaseSearchDto<Newsletter, NewsletterDto>(baseSearchDto, model, mapper);
        }


    }
}
