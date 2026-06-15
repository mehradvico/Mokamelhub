using Application.Common.Dto.Result;
using Application.Services.Language.ProductLangSrv.Iface;
using Application.Services.Language.SeoFieldLangSrv.Dto;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Language.ProductLangSrv.Dto
{
    public class ProductLangSearchDto : BaseSearchDto<SeoFieldLang, SeoFieldLangDto>, IProductLangSearchFields
    {
        public ProductLangSearchDto(ProductLangInputDto dto, IQueryable<SeoFieldLang> list, IMapper mapper) : base(dto, list, mapper)
        {
            ProductId = dto.ProductId;
        }
        public long ProductId { get; set; }
    }
}
