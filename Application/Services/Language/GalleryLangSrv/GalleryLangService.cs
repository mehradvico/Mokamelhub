using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.GalleryLangSrv.Dto;
using Application.Services.Language.GalleryLangSrv.Iface;
using Application.Services.Language.SeoFieldLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Language.GalleryLangSrv
{
    public class GalleryLangService : IGalleryLangService
    {
        private readonly ISeoFieldLangService seoFieldLangService;
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public GalleryLangService(IDataBaseContext _context, IMapper mapper, ISeoFieldLangService seoFieldLangService)
        {
            this._context = _context;
            this.mapper = mapper;
            this.seoFieldLangService = seoFieldLangService;
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
        public GalleryLangSearchDto SearchDto(GalleryLangInputDto searchDto)
        {
            var Gallery = _context.Galleries.Include(s => s.SeoFieldLangs).ThenInclude(s => s.Language).FirstOrDefault(s => s.Id.Equals(searchDto.GalleryId));
            if (Gallery != null)
            {
                var model = Gallery.SeoFieldLangs.AsQueryable();
                return new GalleryLangSearchDto(searchDto, model, mapper);
            }
            IQueryable<SeoFieldLang> emptyModel = Enumerable.Empty<SeoFieldLang>().AsQueryable();
            return new GalleryLangSearchDto(searchDto, emptyModel, mapper);
        }

        public async Task<BaseResultDto> InsertAndUpdateAsyncDto(GalleryLangDto dto)
        {
            var Gallery = await _context.Galleries.AsTracking().Include(s => s.SeoFieldLangs).FirstOrDefaultAsync(s => s.Id.Equals(dto.GalleryId));
            if (Gallery != null)
            {
                var langItem = Gallery.SeoFieldLangs.FirstOrDefault(s => s.LanguageId == dto.SeoFieldLangDto.LanguageId);
                if (langItem == null)
                {

                    langItem = mapper.Map<SeoFieldLang>(dto.SeoFieldLangDto);
                    _context.SeoFieldLangs.Add(langItem);
                    Gallery.SeoFieldLangs.Add(langItem);
                    _context.SaveChanges();
                    dto.SeoFieldLangDto.Id = langItem.Id;
                    return new BaseResultDto<GalleryLangDto>(true, dto);
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
        public BaseResultDto DeleteDto(OtherLangDeleteDto dto)
        {
            try
            {
                var Gallery = _context.Galleries.Include(s => s.SeoFieldLangs).FirstOrDefault(s => s.Id.Equals(dto.ItemId));
                if (Gallery != null)
                {
                    var seoFieldLang = Gallery.SeoFieldLangs.FirstOrDefault(s => s.Id.Equals(dto.OtherLangId));
                    if (seoFieldLang != null)
                    {
                        Gallery.SeoFieldLangs.Remove(seoFieldLang);
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

    }
}
