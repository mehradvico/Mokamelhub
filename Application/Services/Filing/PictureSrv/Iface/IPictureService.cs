using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Services.Filing.PictureSrv.Dto;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Filing.PictureSrv.Iface
{
    public interface IPictureService
    {
        Task<BaseResultDto> InsertAsyncDto(PictureDto dto);
        Task<BaseResultDto> FindVDtoAsync(long id);

        BaseSearchDto<PictureVDto> Search(BaseInputDto baseSearchDto);

        List<Picture> GetAll();

    }
}
