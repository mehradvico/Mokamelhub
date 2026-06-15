using Application.Common.Dto.Result;
using Application.Services.Language.BrandLangSrv.Iface;
using Application.Services.Language.SeoFieldLangSrv.Dto;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Language.BrandLangSrv.Dto
{
    public class BrandLangSearchDto : BaseSearchDto<SeoFieldLang, SeoFieldLangDto>, IBrandLangSearchFields
    {
        public BrandLangSearchDto(BrandLangInputDto dto, IQueryable<SeoFieldLang> list, IMapper mapper) : base(dto, list, mapper)
        {
            BrandId = dto.BrandId;
        }
        public long BrandId { get; set; }
    }
}
