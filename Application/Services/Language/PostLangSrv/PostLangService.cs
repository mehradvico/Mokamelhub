using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.PostLangSrv.Dto;
using Application.Services.Language.PostLangSrv.Iface;
using Application.Services.Language.SeoFieldLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Language.PostLangSrv
{
    public class PostLangService : IPostLangService
    {
        private readonly ISeoFieldLangService seoFieldLangService;
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public PostLangService(IDataBaseContext _context, IMapper mapper, ISeoFieldLangService seoFieldLangService)
        {
            this._context = _context;
            this.mapper = mapper;
            this.seoFieldLangService = seoFieldLangService;
        }

        public BaseResultDto DeleteDto(OtherLangDeleteDto dto)
        {
            try
            {
                var post = _context.Posts.Include(s => s.SeoFieldLangs).FirstOrDefault(s => s.Id.Equals(dto.ItemId));
                if (post != null)
                {
                    var seoFieldLang = post.SeoFieldLangs.FirstOrDefault(s => s.Id.Equals(dto.OtherLangId));
                    if (seoFieldLang != null)
                    {
                        post.SeoFieldLangs.Remove(seoFieldLang);
                        _context.SeoFieldLangs.Remove(seoFieldLang);
                        _context.SaveChanges();
                        return new BaseResultDto(true);
                    }

                }
                return new BaseResultDto<OtherLangDeleteDto>(false, Resource.Notification.InvalidData, dto);
            }
            catch (Exception)
            {
                return new BaseResultDto(false);
            }
        }

        public async Task<BaseResultDto> FindAsyncDto(long id)
        {
            try
            {
                var result = await seoFieldLangService.FindAsyncDto(id);
                return result;
            }
            catch (Exception)
            {

                return new BaseResultDto(false);
            }
        }


        public async Task<BaseResultDto> InsertAndUpdateAsyncDto(PostLangDto dto)
        {
            var post = await _context.Posts.AsTracking().Include(s => s.SeoFieldLangs).FirstOrDefaultAsync(s => s.Id.Equals(dto.PostId));
            if (post != null)
            {
                var langItem = post.SeoFieldLangs.FirstOrDefault(s => s.LanguageId == dto.SeoLangDto.LanguageId);
                if (langItem == null)
                {
                    // var item = mapper.Map<SeoFieldLang>(dto.SeoFieldLangDto);
                    langItem = mapper.Map<SeoFieldLang>(dto.SeoLangDto);
                    _context.SeoFieldLangs.Add(langItem);
                    post.SeoFieldLangs.Add(langItem);
                    _context.SaveChanges();
                    dto.SeoLangDto.Id = langItem.Id;
                    return new BaseResultDto<PostLangDto>(true, dto);
                }
                else
                {
                    var item = mapper.Map(dto, langItem);
                    _context.SeoFieldLangs.Update(item);
                    _context.SaveChanges();
                    return new BaseResultDto(true);
                }

            }
            return new BaseResultDto(false);

        }

        public PostLangSearchDto SearchDto(PostLangInputDto searchDto)
        {
            var post = _context.Posts.Include(s => s.SeoFieldLangs).ThenInclude(s => s.Language).FirstOrDefault(s => s.Id.Equals(searchDto.PostId));
            if (post != null)
            {
                var model = post.SeoFieldLangs.AsQueryable();
                return new PostLangSearchDto(searchDto, model, mapper);
            }
            IQueryable<SeoFieldLang> emptyModel = Enumerable.Empty<SeoFieldLang>().AsQueryable();
            return new PostLangSearchDto(searchDto, emptyModel, mapper);
        }

    }
}
