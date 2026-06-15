using Application.Common.Dto.Result;
using Application.Services.Language.CategoryLangSrv.Iface;
using Application.Services.Language.SeoFieldLangSrv.Dto;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Language.CategoryLangSrv.Dto
{
    public class CategoryLangSearchDto : BaseSearchDto<SeoFieldLang, SeoFieldLangDto>, ICategoryLangSearchFields
    {
        public CategoryLangSearchDto(CategoryLangInputDto dto, IQueryable<SeoFieldLang> list, IMapper mapper) : base(dto, list, mapper)
        {
            CategoryId = dto.CategoryId;
        }
        public long CategoryId { get; set; }
    }
}
