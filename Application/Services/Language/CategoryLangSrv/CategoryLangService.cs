using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.CategoryLangSrv.Dto;
using Application.Services.Language.CategoryLangSrv.Iface;
using Application.Services.Language.SeoFieldLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Language.CategoryLangSrv
{
    public class CategoryLangService : ICategoryLangService
    {
        private readonly ISeoFieldLangService seoFieldLangService;
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public CategoryLangService(ISeoFieldLangService seoFieldLangService, IDataBaseContext _context, IMapper mapper)
        {
            this.seoFieldLangService = seoFieldLangService;
            this._context = _context;
            this.mapper = mapper;
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

        public async Task<BaseResultDto> InsertAndUpdateAsyncDto(CategoryLangDto dto)
        {
            try
            {
                var category = await _context.Categories.AsTracking().Include(s => s.SeoFieldLangs).FirstOrDefaultAsync(s => s.Id.Equals(dto.CategoryId));
                if (category != null)
                {
                    var langItem = category.SeoFieldLangs.FirstOrDefault(s => s.LanguageId == dto.SeoFieldLangDto.LanguageId);
                    if (langItem == null)
                    {
                        langItem = mapper.Map<SeoFieldLang>(dto.SeoFieldLangDto);
                        await _context.SeoFieldLangs.AddAsync(langItem);
                        category.SeoFieldLangs.Add(langItem);
                        _context.SaveChanges();
                        dto.SeoFieldLangDto.Id = langItem.Id;
                        return new BaseResultDto<CategoryLangDto>(true, dto);
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
            catch (Exception)
            {

                return new BaseResultDto(false);
            }
        }
        public BaseResultDto DeleteDto(OtherLangDeleteDto dto)
        {
            try
            {
                var category = _context.Categories.Include(s => s.SeoFieldLangs).FirstOrDefault(o => o.Id.Equals(dto.ItemId));
                if (category != null)
                {
                    var seoFieldLang = category.SeoFieldLangs.FirstOrDefault(s => s.Id.Equals(dto.OtherLangId));
                    if (seoFieldLang != null)
                    {
                        category.SeoFieldLangs.Remove(seoFieldLang);
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

        public CategoryLangSearchDto SearchDto(CategoryLangInputDto dto)
        {
            try
            {
                var category = _context.Categories.Include(s => s.SeoFieldLangs).ThenInclude(o => o.Language).FirstOrDefault(s => s.Id.Equals(dto.CategoryId));
                if (category != null)
                {
                    var model = category.SeoFieldLangs.AsQueryable();
                    return new CategoryLangSearchDto(dto, model, mapper);
                }
                IQueryable<SeoFieldLang> emptyModel = Enumerable.Empty<SeoFieldLang>().AsQueryable();
                return new CategoryLangSearchDto(dto, emptyModel, mapper);
            }
            catch (Exception)
            {
                IQueryable<SeoFieldLang> emptyModel = Enumerable.Empty<SeoFieldLang>().AsQueryable();
                return new CategoryLangSearchDto(dto, emptyModel, mapper);
            }
        }


    }
}
