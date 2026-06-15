using Application.Common.Dto.Input;
using Application.Services.Content.GallerySrv.Iface;

namespace Application.Services.Content.GallerySrv.Dto
{
    public class GalleryInputDto : BaseInputDto, IGallerySearchFields
    {
        public long? CategoryId { get; set; }
        public string CategoryLabel { get; set; }
    }
}
