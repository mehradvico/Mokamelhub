using Application.Common.Dto.Result;
using Application.Services.Content.GallerySrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Content.GallerySrv.Dto
{
    public class GallerySearchDto : BaseSearchDto<Gallery, GalleryVDto>, IGallerySearchFields
    {
        public GallerySearchDto(GalleryInputDto dto, IQueryable<Gallery> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.CategoryId = dto.CategoryId;
            this.CategoryLabel = dto.CategoryLabel;
        }

        public long? CategoryId { get; set; }
        public string CategoryLabel { get; set; }

    }
}
