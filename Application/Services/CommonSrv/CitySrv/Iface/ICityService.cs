using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.CommonSrv.CitySrv.Dto;
using Entities.Entities;

namespace Application.Services.CommonSrv.CitySrv.Iface
{
    public interface ICityService : ICommonSrv<City, CityDto>
    {
        BaseSearchDto<CityVDto> Search(CityInputDto baseSearchDto);
        BaseResultDto GetAll();

    }
}
