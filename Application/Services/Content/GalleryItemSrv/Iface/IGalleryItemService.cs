using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Content.GalleryItemSrv.Dto;
using Entities.Entities;

namespace Application.Services.Content.GalleryItemSrv.Iface
{
    public interface IGalleryItemService : ICommonSrv<GalleryItem, GalleryItemDto>
    {
        BaseSearchDto<GalleryItemDto> Search(GalleryItemInputDto baseSearchDto);
    }
}
