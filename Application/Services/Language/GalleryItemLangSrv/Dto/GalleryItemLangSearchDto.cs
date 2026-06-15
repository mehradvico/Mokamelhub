using Application.Common.Dto.Result;
using Application.Services.Language.FullNameFieldLangSrv.Dto;
using Application.Services.Language.GalleryItemLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Language.GalleryItemLangSrv.Dto
{
    public class GalleryItemLangSearchDto : BaseSearchDto<FullNameFieldLang, FullNameFieldLangDto>, IGalleryItemLangSearchFields
    {
        public GalleryItemLangSearchDto(GalleryItemLangInputDto dto, IQueryable<FullNameFieldLang> list, IMapper mapper) : base(dto, list, mapper)
        {
            GalleryItemId = dto.GalleryItemId;
        }

        public long GalleryItemId { get; set; }
    }
}
