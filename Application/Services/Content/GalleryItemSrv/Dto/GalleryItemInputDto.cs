using Application.Common.Dto.Input;
using Application.Services.Content.GallerySrv.Iface;

namespace Application.Services.Content.GalleryItemSrv.Dto
{
    public class GalleryItemInputDto : BaseInputDto, IGalleryItemSearchFields
    {
        public long GalleryId { get; set; }
    }
}
