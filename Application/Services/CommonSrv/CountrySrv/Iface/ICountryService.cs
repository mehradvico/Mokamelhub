using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.CommonSrv.CountrySrv.Dto;
using Entities.Entities;

namespace Application.Services.CommonSrv.CountrySrv.Iface
{
    public interface ICountryService : ICommonSrv<Country, CountryDto>
    {
        BaseSearchDto<CountryVDto> Search(BaseInputDto baseSearchDto);
    }
}
