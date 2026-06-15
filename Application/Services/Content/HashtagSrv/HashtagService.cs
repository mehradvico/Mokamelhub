using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Service;
using Application.Services.Content.HashtagSrv.Dto;
using Application.Services.Content.HashtagSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Content.HashtagSrv
{
    public class HashtagService : CommonSrv<Hashtag, HashtagDto>, IHashtagService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;

        public HashtagService(IDataBaseContext _context, IMapper mapper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }

        public Hashtag GetOrAddByName(string name)
        {
            var item = _context.Hashtags.Include(s => s.posts).AsTracking().FirstOrDefault(s => s.Name == name);
            if (item == null)
            {
                item = new Hashtag { Name = name };
                _context.Hashtags.Add(item);
                _context.SaveChanges();
                item.posts = new List<Post>();

            }
            return item;
        }

        public BaseSearchDto<HashtagDto> Search(BaseInputDto baseSearchDto)
        {
            var model = _context.Hashtags.AsQueryable();
            if (!string.IsNullOrEmpty(baseSearchDto.Q))
            {
                model = model.Where(s => s.Name.Contains(baseSearchDto.Q));
            }
            model = model.OrderByDescending(o => o.Id).AsQueryable();
            return new BaseSearchDto<Hashtag, HashtagDto>(baseSearchDto, model, mapper);
        }
    }
}
