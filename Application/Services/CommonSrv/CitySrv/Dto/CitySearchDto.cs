using Application.Common.Dto.Result;
using Application.Services.CommonSrv.CitySrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.CommonSrv.CitySrv.Dto
{
    public class CitySearchDto : BaseSearchDto<City, CityVDto>, ICitySearchFields
    {
        public CitySearchDto(CityInputDto dto, IQueryable<City> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.StateId = dto.StateId;
        }


        public long StateId { get; set; }
    }
}
