using Application.Common.Context;
using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.SeoFieldLangSrv.Dto;
using Application.Services.Language.SeoFieldLangSrv.Iface;
using Application.Services.Language.StoreLangSrv.Dto;
using Application.Services.Language.StoreLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Language.StoreLangSrv
{
    public class StoreLangService : IStoreLangService
    {
        private readonly ISeoFieldLangService seoFieldLangService;
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public StoreLangService(ISeoFieldLangService seoFieldLangService, IDataBaseContext _context, IMapper mapper)
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

        public async Task<BaseResultDto> InsertAndUpdateAsyncDto(StoreLangDto dto)
        {
            try
            {
                var store = await _context.Stores.AsTracking().Include(s => s.SeoFieldLangs).FirstOrDefaultAsync(s => s.Id.Equals(dto.StoreId));
                if (store != null)
                {
                    var langItem = store.SeoFieldLangs.FirstOrDefault(s => s.LanguageId == dto.SeoFieldLangDto.LanguageId);
                    if (langItem == null)
                    {
                        langItem = mapper.Map<SeoFieldLang>(dto.SeoFieldLangDto);
                        await _context.SeoFieldLangs.AddAsync(langItem);
                        store.SeoFieldLangs.Add(langItem);
                        _context.SaveChanges();
                        dto.SeoFieldLangDto.Id = langItem.Id;
                        return new BaseResultDto<StoreLangDto>(true, dto);
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
                var store = _context.Stores.Include(s => s.SeoFieldLangs).FirstOrDefault(o => o.Id.Equals(dto.ItemId));
                if (store != null)
                {
                    var seoFieldLang = store.SeoFieldLangs.FirstOrDefault(s => s.Id.Equals(dto.OtherLangId));
                    if (seoFieldLang != null)
                    {
                        store.SeoFieldLangs.Remove(seoFieldLang);
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

        public StoreLangSearchDto SearchDto(StoreLangInputDto inputDto)
        {
            try
            {
                var store = _context.Stores.Include(s => s.SeoFieldLangs).ThenInclude(o => o.Language).FirstOrDefault(s => s.Id.Equals(inputDto.StoreId));
                if (store != null)
                {
                    var model = store.SeoFieldLangs.AsQueryable();
                    return new StoreLangSearchDto(inputDto, model, mapper);
                }
                IQueryable<SeoFieldLang> emptyModel = Enumerable.Empty<SeoFieldLang>().AsQueryable();
                return new StoreLangSearchDto(inputDto, emptyModel, mapper);
            }
            catch (Exception)
            {
                IQueryable<SeoFieldLang> emptyModel = Enumerable.Empty<SeoFieldLang>().AsQueryable();
                return new StoreLangSearchDto(inputDto, emptyModel, mapper);

            }
        }
    }
}
