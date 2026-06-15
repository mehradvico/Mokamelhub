using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.CodeLangSrv.Dto;
using Application.Services.Language.CodeLangSrv.Iface;
using Application.Services.Language.NameFieldLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Language.CodeLangSrv
{
    public class CodeLangService : ICodeLangService
    {
        private readonly INameFieldLangService nameFieldLangService;
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public CodeLangService(INameFieldLangService nameFieldLangService, IDataBaseContext _context, IMapper mapper)
        {
            this.nameFieldLangService = nameFieldLangService;
            this._context = _context;
            this.mapper = mapper;
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

        public async Task<BaseResultDto> InsertAndUpdateDto(CodeLangDto dto)
        {
            try
            {
                var code = await _context.Codes.AsTracking().Include(s => s.NameFieldLangs).FirstOrDefaultAsync(s => s.Id.Equals(dto.CodeId));
                if (code != null)
                {
                    var langItem = code.NameFieldLangs.FirstOrDefault(s => s.LanguageId.Equals(dto.NameFieldLangDto.LanguageId));
                    if (langItem == null)
                    {
                        langItem = mapper.Map<NameFieldLang>(dto.NameFieldLangDto);
                        await _context.NameFieldLangs.AddAsync(langItem);
                        code.NameFieldLangs.Add(langItem);
                        _context.SaveChanges();
                        dto.NameFieldLangDto.Id = langItem.Id;
                        return new BaseResultDto<CodeLangDto>(true, dto);
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

        public CodeLangSearchDto SearchDto(CodeLangInputDto dto)
        {
            try
            {
                var code = _context.Codes.Include(s => s.NameFieldLangs).ThenInclude(o => o.Language).FirstOrDefault(s => s.Id.Equals(dto.CodeId));
                if (code != null)
                {
                    var model = code.NameFieldLangs.AsQueryable();
                    return new CodeLangSearchDto(dto, model, mapper);
                }
                IQueryable<NameFieldLang> emptyModel = Enumerable.Empty<NameFieldLang>().AsQueryable();
                return new CodeLangSearchDto(dto, emptyModel, mapper);
            }
            catch (Exception)
            {
                IQueryable<NameFieldLang> emptyModel = Enumerable.Empty<NameFieldLang>().AsQueryable();
                return new CodeLangSearchDto(dto, emptyModel, mapper);
            }
        }
        public BaseResultDto DeleteDto(OtherLangDeleteDto dto)
        {
            try
            {
                var code = _context.Codes.Include(s => s.NameFieldLangs).FirstOrDefault(s => s.Id.Equals(dto.ItemId));
                if (code != null)
                {
                    var nameFieldLang = code.NameFieldLangs.FirstOrDefault(s => s.Id.Equals(dto.OtherLangId));
                    if (nameFieldLang != null)
                    {
                        code.NameFieldLangs.Remove(nameFieldLang);
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
