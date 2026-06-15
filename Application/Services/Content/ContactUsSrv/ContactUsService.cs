using Application.Common.Dto.Result;
using Application.Common.Helpers;
using Application.Services.Content.ContactUsSrv.Dto;
using Application.Services.Content.ContactUsSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Content.ContactUsSrv
{
    public class ContactUsService : IContactUsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public ContactUsService(IDataBaseContext _context, IMapper mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }
        public async Task<BaseResultDto<ContactUsVDto>> FindAsyncVDto(long id)
        {
            var item = await _context.ContactUses.FirstOrDefaultAsync(s => s.Id == id);

            return new BaseResultDto<ContactUsVDto>(true, mapper.Map<ContactUsVDto>(item));

        }
        public async Task<BaseResultDto<ContactUsDto>> FindAsyncDto(long id)
        {
            var item = await _context.ContactUses.FirstOrDefaultAsync(s => s.Id == id);

            return new BaseResultDto<ContactUsDto>(true, mapper.Map<ContactUsDto>(item));

        }
        public ContactUsSearchDto Search(ContactUsInputDto baseSearchDto)
        {
            var model = _context.ContactUses.Include(s => s.File).AsQueryable();
            if (!string.IsNullOrEmpty(baseSearchDto.Q))
            {
                model = model.Where(s => s.FullName.Contains(baseSearchDto.Q));
            }
            if (baseSearchDto.Status.HasValue)
            {
                model = model.Where(s => s.Status == baseSearchDto.Status);
            }
            if (baseSearchDto.ContactUsGroupId.HasValue)
            {
                model = model.Where(s => s.ContactUsGroupId == baseSearchDto.ContactUsGroupId);
            }
            switch (baseSearchDto.SortBy)
            {
                case Common.Enumerable.SortEnum.Default:
                    {
                        model = model.OrderByDescending(s => s.Id);
                        break;
                    }
                case Common.Enumerable.SortEnum.New:
                    {
                        model = model.OrderByDescending(s => s.Id);
                        break;
                    }
                case Common.Enumerable.SortEnum.Old:
                    {
                        model = model.OrderBy(s => s.Id);
                        break;
                    }

                default:
                    break;

            }

            return new ContactUsSearchDto(baseSearchDto, model, mapper);
        }

        public async Task<BaseResultDto> InsertAsyncDto(ContactUsDto dto)
        {
            try
            {
                dto.Body = await SanitizeTextHelper.ToSanitizeAsync(dto.Body);
                var modelCheker = ModelHelper<ContactUsDto>.ModelErrors(dto);
                if (!modelCheker.IsSuccess)
                {
                    return modelCheker;
                }
                else
                {
                    dto.Body = await SanitizeTextHelper.ToSanitizeAsync(dto.Body);
                    foreach (var cItem in dto.ContactUsItems)
                    {
                        cItem.Title = await SanitizeTextHelper.ToSanitizeAsync(cItem.Title);
                        cItem.Value = await SanitizeTextHelper.ToSanitizeAsync(cItem.Value);
                    }
                    var item = mapper.Map<ContactUs>(dto);
                    item.Answer = null;
                    item.Status = false;
                    item.CreateDate = DateTime.Now;
                    await _context.ContactUses.AddAsync(item);
                    _context.SaveChanges();
                    return new BaseResultDto(true);
                }

            }
            catch (Exception ex)
            {
                return new BaseResultDto<ContactUsDto>(isSuccess: false, val: ex.Message, data: dto);
            }
        }
        public BaseResultDto Update(ContactUsDto dto)
        {

            var item = _context.ContactUses.FirstOrDefault(s => s.Id == dto.Id);
            item.Answer = dto.Answer;
            item.Status = dto.Status;
            _context.ContactUses.Update(item);
            _context.SaveChanges();
            return new BaseResultDto(true);
        }


    }
}
