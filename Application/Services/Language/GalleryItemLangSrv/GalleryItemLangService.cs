using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.FullNameFieldLangSrv.Iface;
using Application.Services.Language.GalleryItemLangSrv.Dto;
using Application.Services.Language.GalleryItemLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Language.GalleryItemLangSrv
{
    public class GalleryItemLangService : IGalleryItemLangService
    {
        private readonly IFullNameFieldLangService fullNameFieldLangService;
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public GalleryItemLangService(IDataBaseContext _context, IMapper mapper, IFullNameFieldLangService fullNameFieldLangService)
        {
            this._context = _context;
            this.mapper = mapper;
            this.fullNameFieldLangService = fullNameFieldLangService;
        }

        public BaseResultDto DeleteDto(OtherLangDeleteDto dto)
        {
            try
            {
                var GalleryItem = _context.GalleryItems.Include(s => s.FullNameFieldLangs).FirstOrDefault(s => s.Id.Equals(dto.ItemId));
                if (GalleryItem != null)
                {
                    var fullNameFieldLang = GalleryItem.FullNameFieldLangs.FirstOrDefault(s => s.Id.Equals(dto.OtherLangId));
                    if (fullNameFieldLang != null)
                    {
                        GalleryItem.FullNameFieldLangs.Remove(fullNameFieldLang);
                        _context.FullNameFieldLangs.Remove(fullNameFieldLang);
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
                var result = await fullNameFieldLangService.FindAsyncDto(id);
                return result;
            }
            catch (Exception)
            {

                return new BaseResultDto(false);
            }
        }

        public Task<BaseResultDto> InsertAndUpdateAsyncDto(GalleryItemLangDto dto)
        {
            throw new NotImplementedException();
        }

        public GalleryItemLangSearchDto SearchDto(GalleryItemLangInputDto searchDto)
        {
            var GalleryItem = _context.GalleryItems.Include(s => s.FullNameFieldLangs).ThenInclude(s => s.Language).FirstOrDefault(s => s.Id.Equals(searchDto.GalleryItemId));
            if (GalleryItem != null)
            {
                var model = GalleryItem.FullNameFieldLangs.AsQueryable();
                return new GalleryItemLangSearchDto(searchDto, model, mapper);
            }
            IQueryable<FullNameFieldLang> emptyModel = Enumerable.Empty<FullNameFieldLang>().AsQueryable();
            return new GalleryItemLangSearchDto(searchDto, emptyModel, mapper);
        }
    }
}
