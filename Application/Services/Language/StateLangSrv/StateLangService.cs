using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.NameFieldLangSrv.Iface;
using Application.Services.Language.StateLangSrv.Dto;
using Application.Services.Language.StateLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Language.StateLangSrv
{
    public class StateLangService : IStateLangService
    {
        private readonly INameFieldLangService nameFieldLangService;
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public StateLangService(INameFieldLangService nameFieldLangService, IDataBaseContext _context, IMapper mapper)
        {
            this.nameFieldLangService = nameFieldLangService;
            this.mapper = mapper;
            this._context = _context;
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
        public StateLangSearchDto SearchDto(StateLangInputDto dto)
        {
            try
            {
                var State = _context.States.Include(s => s.NameFieldLangs).ThenInclude(o => o.Language).FirstOrDefault(s => s.Id.Equals(dto.StateId));
                if (State != null)
                {
                    var model = State.NameFieldLangs.AsQueryable();
                    return new StateLangSearchDto(dto, model, mapper);
                }
                IQueryable<NameFieldLang> emptyModel = Enumerable.Empty<NameFieldLang>().AsQueryable();
                return new StateLangSearchDto(dto, emptyModel, mapper);
            }
            catch (Exception)
            {
                IQueryable<NameFieldLang> emptyModel = Enumerable.Empty<NameFieldLang>().AsQueryable();
                return new StateLangSearchDto(dto, emptyModel, mapper);

            }
        }

        public async Task<BaseResultDto> InsertAndUpdateAsyncDto(StateLangDto dto)
        {
            try
            {
                var State = await _context.States.AsTracking().Include(x => x.NameFieldLangs).FirstOrDefaultAsync(x => x.Id.Equals(dto.StateId));
                if (State != null)
                {
                    var langItem = State.NameFieldLangs.FirstOrDefault(x => x.LanguageId.Equals(dto.NameFieldLangDto.LanguageId));
                    if (langItem == null)
                    {
                        langItem = mapper.Map<NameFieldLang>(dto.NameFieldLangDto);
                        await _context.NameFieldLangs.AddAsync(langItem);
                        State.NameFieldLangs.Add(langItem);
                        _context.SaveChanges();
                        dto.NameFieldLangDto.Id = langItem.Id;
                        return new BaseResultDto<StateLangDto>(true, dto);
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
        public BaseResultDto DeleteDto(OtherLangDeleteDto dto)
        {
            try
            {
                var State = _context.States.Include(s => s.NameFieldLangs).FirstOrDefault(s => s.Id.Equals(dto.ItemId));
                if (State != null)
                {
                    var nameFieldLang = State.NameFieldLangs.FirstOrDefault(s => s.Id.Equals(dto.OtherLangId));
                    if (nameFieldLang != null)
                    {
                        State.NameFieldLangs.Remove(nameFieldLang);
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
    }
}
