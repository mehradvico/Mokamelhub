using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.FeatureLangSrv.Dto;
using Application.Services.Language.FeatureLangSrv.Iface;
using Application.Services.Language.NameFieldLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Language.FeatureLangSrv
{
    public class FeatureLangService : IFeatureLangService
    {
        private readonly INameFieldLangService nameFieldLangService;
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public FeatureLangService(INameFieldLangService nameFieldLangService, IDataBaseContext _context, IMapper mapper)
        {
            this.nameFieldLangService = nameFieldLangService;
            this.mapper = mapper;
            this._context = _context;
        }

        public BaseResultDto DeleteDto(OtherLangDeleteDto dto)
        {
            try
            {
                var feature = _context.Features.Include(s => s.NameFieldLangs).FirstOrDefault(s => s.Id.Equals(dto.ItemId));
                if (feature != null)
                {
                    var nameFieldLang = feature.NameFieldLangs.FirstOrDefault(s => s.Id.Equals(dto.OtherLangId));
                    if (nameFieldLang != null)
                    {
                        feature.NameFieldLangs.Remove(nameFieldLang);
                        _context.NameFieldLangs.Remove(nameFieldLang);
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
                var result = await nameFieldLangService.FindAsyncDto(id);
                return result;
            }
            catch (Exception)
            {
                return new BaseResultDto(false);
            }
        }

        public async Task<BaseResultDto> InsertAndUpdateAsyncDto(FeatureLangDto dto)
        {
            try
            {
                var feature = await _context.Features.AsTracking().Include(x => x.NameFieldLangs).FirstOrDefaultAsync(x => x.Id.Equals(dto.FeatureId));
                if (feature != null)
                {
                    var langItem = feature.NameFieldLangs.FirstOrDefault(x => x.LanguageId.Equals(dto.NameFieldLangDto.LanguageId));
                    if (langItem == null)
                    {
                        langItem = mapper.Map<NameFieldLang>(dto.NameFieldLangDto);
                        await _context.NameFieldLangs.AddAsync(langItem);
                        feature.NameFieldLangs.Add(langItem);
                        _context.SaveChanges();
                        dto.NameFieldLangDto.Id = langItem.Id;
                        return new BaseResultDto<FeatureLangDto>(true, dto);
                    }
                    else
                    {
                        var item = mapper.Map(dto, langItem);
                        _context.NameFieldLangs.Update(item);
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

        public FeatureLangSearchDto SearchDto(FeatureLangInputDto dto)
        {
            try
            {
                var feature = _context.Features.Include(s => s.NameFieldLangs).ThenInclude(o => o.Language).FirstOrDefault(s => s.Id.Equals(dto.FeatureId));
                if (feature != null)
                {
                    var model = feature.NameFieldLangs.AsQueryable();
                    return new FeatureLangSearchDto(dto, model, mapper);
                }
                IQueryable<NameFieldLang> emptyModel = Enumerable.Empty<NameFieldLang>().AsQueryable();
                return new FeatureLangSearchDto(dto, emptyModel, mapper);
            }
            catch (Exception)
            {
                IQueryable<NameFieldLang> emptyModel = Enumerable.Empty<NameFieldLang>().AsQueryable();
                return new FeatureLangSearchDto(dto, emptyModel, mapper);

            }
        }
    }
}
