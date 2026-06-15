using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.ProductLangSrv.Dto;
using Application.Services.Language.ProductLangSrv.Iface;
using Application.Services.Language.SeoFieldLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Language.ProductLangSrv
{
    public class ProductLangService : IProductLangService
    {
        private readonly ISeoFieldLangService seoFieldLangService;
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public ProductLangService(ISeoFieldLangService seoFieldLangService, IDataBaseContext _context, IMapper mapper)
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
        public ProductLangSearchDto searchDto(ProductLangInputDto dto)
        {
            try
            {
                var product = _context.Products.Include(s => s.SeoFieldLangs).ThenInclude(o => o.Language).FirstOrDefault(x => x.Id.Equals(dto.ProductId));
                if (product != null)
                {
                    var model = product.SeoFieldLangs.AsQueryable();
                    return new ProductLangSearchDto(dto, model, mapper);
                }
                IQueryable<SeoFieldLang> emptyModel = Enumerable.Empty<SeoFieldLang>().AsQueryable();
                return new ProductLangSearchDto(dto, emptyModel, mapper);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<BaseResultDto> InsertAndUpdateDto(ProductLangDto dto)
        {
            try
            {
                var product = await _context.Products.AsTracking().Include(s => s.SeoFieldLangs).FirstOrDefaultAsync(s => s.Id.Equals(dto.ProductId));
                if (product != null)
                {
                    var langItem = product.SeoFieldLangs.FirstOrDefault(s => s.LanguageId == dto.SeoFieldLangDto.LanguageId);
                    if (langItem == null)
                    {
                        langItem = mapper.Map<SeoFieldLang>(dto.SeoFieldLangDto);
                        await _context.SeoFieldLangs.AddAsync(langItem);
                        product.SeoFieldLangs.Add(langItem);
                        _context.SaveChanges();
                        dto.SeoFieldLangDto.Id = langItem.Id;
                        return new BaseResultDto<ProductLangDto>(true, dto);
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
                var product = _context.Products.Include(s => s.SeoFieldLangs).FirstOrDefault(s => s.Id.Equals(dto.ItemId));
                if (product != null)
                {
                    var seoFieldLang = product.SeoFieldLangs.FirstOrDefault(s => s.Id.Equals(dto.OtherLangId));
                    if (seoFieldLang != null)
                    {
                        product.SeoFieldLangs.Remove(seoFieldLang);
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
    }
}
