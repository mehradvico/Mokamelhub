using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.FeatureItemLangSrv.Dto;
using Application.Services.Language.FeatureItemLangSrv.Iface;
using Application.Services.Language.NameFieldLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Language.FeatureItemLangSrv
{
    public class FeatureItemLangService : IFeatureItemLangService
    {
        private readonly INameFieldLangService nameFieldLangService;
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public FeatureItemLangService(INameFieldLangService nameFieldLangService, IDataBaseContext _context, IMapper mapper)
        {
            this.nameFieldLangService = nameFieldLangService;
            this._context = _context;
            this.mapper = mapper;
        }

        public BaseResultDto DeleteDto(OtherLangDeleteDto dto)
        {
            try
            {
                var featureItem = _context.FeatureItems.Include(s => s.NameFieldLangs).FirstOrDefault(s => s.Id.Equals(dto.ItemId));
                if (featureItem != null)
                {
                    var nameFieldLang = featureItem.NameFieldLangs.FirstOrDefault(s => s.Id.Equals(dto.OtherLangId));
                    if (nameFieldLang != null)
                    {
                        featureItem.NameFieldLangs.Remove(nameFieldLang);
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

        public async Task<BaseResultDto> InsertAndUpdateAsyncDto(FeatureItemLangDto dto)
        {
            try
            {
                var featureItem = await _context.FeatureItems.AsTracking().Include(x => x.NameFieldLangs).FirstOrDefaultAsync(x => x.Id.Equals(dto.FeatureItemId));
                if (featureItem != null)
                {
                    var langItem = featureItem.NameFieldLangs.FirstOrDefault(x => x.LanguageId.Equals(dto.NameFieldLangDto.LanguageId));
                    if (langItem == null)
                    {
                        langItem = mapper.Map<NameFieldLang>(dto.NameFieldLangDto);
                        await _context.NameFieldLangs.AddAsync(langItem);
                        featureItem.NameFieldLangs.Add(langItem);
                        _context.SaveChanges();
                        dto.NameFieldLangDto.Id = langItem.Id;
                        return new BaseResultDto<FeatureItemLangDto>(true, dto);
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

        public FeatureItemLangSearchDto SearchDto(FeatureItemLangInputDto dto)
        {
            try
            {
                var featureItem = _context.FeatureItems.Include(s => s.NameFieldLangs).ThenInclude(o => o.Language).FirstOrDefault(s => s.Id.Equals(dto.FeatureItemId));
                if (featureItem != null)
                {
                    var model = featureItem.NameFieldLangs.AsQueryable();
                    return new FeatureItemLangSearchDto(dto, model, mapper);
                }
                IQueryable<NameFieldLang> emptyModel = Enumerable.Empty<NameFieldLang>().AsQueryable();
                return new FeatureItemLangSearchDto(dto, emptyModel, mapper);
            }
            catch (Exception)
            {
                IQueryable<NameFieldLang> emptyModel = Enumerable.Empty<NameFieldLang>().AsQueryable();
                return new FeatureItemLangSearchDto(dto, emptyModel, mapper);

            }
        }
    }
}
