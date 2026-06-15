using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.CityLangSrv.Dto;
using Application.Services.Language.CityLangSrv.Iface;
using Application.Services.Language.NameFieldLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Language.CityLangSrv
{
    public class CityLangService : ICityLangService
    {
        private readonly INameFieldLangService nameFieldLangService;
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public CityLangService(INameFieldLangService nameFieldLangService, IDataBaseContext _context, IMapper mapper)
        {
            this.nameFieldLangService = nameFieldLangService;
            this.mapper = mapper;
            this._context = _context;
        }

        public BaseResultDto DeleteDto(OtherLangDeleteDto dto)
        {
            try
            {
                var City = _context.Cities.Include(s => s.NameFieldLangs).FirstOrDefault(s => s.Id.Equals(dto.ItemId));
                if (City != null)
                {
                    var nameFieldLang = City.NameFieldLangs.FirstOrDefault(s => s.Id.Equals(dto.OtherLangId));
                    if (nameFieldLang != null)
                    {
                        City.NameFieldLangs.Remove(nameFieldLang);
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

        public async Task<BaseResultDto> InsertAndUpdateAsyncDto(CityLangDto dto)
        {
            try
            {
                var City = await _context.Cities.AsTracking().Include(x => x.NameFieldLangs).FirstOrDefaultAsync(x => x.Id.Equals(dto.CityId));
                if (City != null)
                {
                    var langItem = City.NameFieldLangs.FirstOrDefault(x => x.LanguageId.Equals(dto.NameFieldLangDto.LanguageId));
                    if (langItem == null)
                    {
                        langItem = mapper.Map<NameFieldLang>(dto.NameFieldLangDto);
                        await _context.NameFieldLangs.AddAsync(langItem);
                        City.NameFieldLangs.Add(langItem);
                        _context.SaveChanges();
                        dto.NameFieldLangDto.Id = langItem.Id;
                        return new BaseResultDto<CityLangDto>(true, dto);
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

        public CityLangSearchDto SearchDto(CityLangInputDto dto)
        {
            try
            {
                var City = _context.Cities.Include(s => s.NameFieldLangs).ThenInclude(o => o.Language).FirstOrDefault(s => s.Id.Equals(dto.CityId));
                if (City != null)
                {
                    var model = City.NameFieldLangs.AsQueryable();
                    return new CityLangSearchDto(dto, model, mapper);
                }
                IQueryable<NameFieldLang> emptyModel = Enumerable.Empty<NameFieldLang>().AsQueryable();
                return new CityLangSearchDto(dto, emptyModel, mapper);
            }
            catch (Exception)
            {
                IQueryable<NameFieldLang> emptyModel = Enumerable.Empty<NameFieldLang>().AsQueryable();
                return new CityLangSearchDto(dto, emptyModel, mapper);

            }
        }
    }
}
