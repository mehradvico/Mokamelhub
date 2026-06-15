using Application.Common.Dto.Input;
using Application.Services.Language.GalleryItemLangSrv.Iface;

namespace Application.Services.Language.GalleryItemLangSrv.Dto
{
    public class GalleryItemLangInputDto : BaseInputDto, IGalleryItemLangSearchFields
    {
        public long GalleryItemId { get; set; }
    }
}
