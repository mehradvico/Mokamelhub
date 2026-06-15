using Application.Common.Dto.Result;
using Application.Services.Language.GalleryLangSrv.Iface;
using Application.Services.Language.SeoFieldLangSrv.Dto;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Language.GalleryLangSrv.Dto
{
    public class GalleryLangSearchDto : BaseSearchDto<SeoFieldLang, SeoFieldLangDto>, IGalleryLangSearchFields
    {
        public GalleryLangSearchDto(GalleryLangInputDto dto, IQueryable<SeoFieldLang> list, IMapper mapper) : base(dto, list, mapper)
        {
            GalleryId = dto.GalleryId;
        }

        public long GalleryId { get; set; }
    }
}
