using Application.Common.Dto.Result;
using Application.Services.Content.GallerySrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Content.GalleryItemSrv.Dto
{
    public class GalleryItemSearchDto : BaseSearchDto<GalleryItem, GalleryItemVDto>, IGalleryItemSearchFields
    {
        public GalleryItemSearchDto(GalleryItemInputDto dto, IQueryable<GalleryItem> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.GalleryId = dto.GalleryId;
        }


        public long GalleryId { get; set; }
    }
}
