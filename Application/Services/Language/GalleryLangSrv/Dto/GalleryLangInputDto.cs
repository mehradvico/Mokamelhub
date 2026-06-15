using Application.Common.Dto.Input;
using Application.Services.Language.GalleryLangSrv.Iface;

namespace Application.Services.Language.GalleryLangSrv.Dto
{
    public class GalleryLangInputDto : BaseInputDto, IGalleryLangSearchFields
    {
        public long GalleryId { get; set; }
    }
}
