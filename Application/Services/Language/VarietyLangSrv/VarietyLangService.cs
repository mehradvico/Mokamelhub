using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.NameFieldLangSrv.Iface;
using Application.Services.Language.VarietyLangSrv.Dto;
using Application.Services.Language.VarietyLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Language.VarietyLangSrv
{
    public class VarietyLangService : IVarietyLangService
    {
        private readonly INameFieldLangService nameFieldLangService;
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public VarietyLangService(INameFieldLangService nameFieldLangService, IDataBaseContext _context, IMapper mapper)
        {
            this.nameFieldLangService = nameFieldLangService;
            this.mapper = mapper;
            this._context = _context;
        }

        public BaseResultDto DeleteDto(OtherLangDeleteDto dto)
        {
            try
            {
                var variety = _context.Varieties.Include(s => s.NameFieldLangs).FirstOrDefault(s => s.Id.Equals(dto.ItemId));
                if (variety != null)
                {
                    var nameFieldLang = variety.NameFieldLangs.FirstOrDefault(s => s.Id.Equals(dto.OtherLangId));
                    if (nameFieldLang != null)
                    {
                        variety.NameFieldLangs.Remove(nameFieldLang);
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

        public async Task<BaseResultDto> InsertAndUpdateAsyncDto(VarietyLangDto dto)
        {
            try
            {
                var variety = await _context.Varieties.AsTracking().Include(x => x.NameFieldLangs).FirstOrDefaultAsync(x => x.Id.Equals(dto.VarietyId));
                if (variety != null)
                {
                    var langItem = variety.NameFieldLangs.FirstOrDefault(x => x.LanguageId.Equals(dto.NameFieldLangDto.LanguageId));
                    if (langItem == null)
                    {
                        langItem = mapper.Map<NameFieldLang>(dto.NameFieldLangDto);
                        await _context.NameFieldLangs.AddAsync(langItem);
                        variety.NameFieldLangs.Add(langItem);
                        _context.SaveChanges();
                        dto.NameFieldLangDto.Id = langItem.Id;
                        return new BaseResultDto<VarietyLangDto>(true, dto);
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

        public VarietyLangSearchDto SearchDto(VarietyLangInputDto dto)
        {
            try
            {
                var variety = _context.Varieties.Include(s => s.NameFieldLangs).ThenInclude(o => o.Language).FirstOrDefault(s => s.Id.Equals(dto.VarietyId));
                if (variety != null)
                {
                    var model = variety.NameFieldLangs.AsQueryable();
                    return new VarietyLangSearchDto(dto, model, mapper);
                }
                IQueryable<NameFieldLang> emptyModel = Enumerable.Empty<NameFieldLang>().AsQueryable();
                return new VarietyLangSearchDto(dto, emptyModel, mapper);
            }
            catch (Exception)
            {
                IQueryable<NameFieldLang> emptyModel = Enumerable.Empty<NameFieldLang>().AsQueryable();
                return new VarietyLangSearchDto(dto, emptyModel, mapper);

            }
        }
    }
}
