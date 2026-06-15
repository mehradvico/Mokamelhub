using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Content.GallerySrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.Content.GallerySrv.Iface
{
    public interface IGalleryService : ICommonSrv<Gallery, GalleryDto>
    {
        Task<BaseResultDto<GalleryVDto>> FindVDtoAsync(string label);

        BaseSearchDto<GalleryVDto> Search(GalleryInputDto baseSearchDto);

    }
}
