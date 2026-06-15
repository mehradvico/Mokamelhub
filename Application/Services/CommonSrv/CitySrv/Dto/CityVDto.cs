using Application.Common.Dto.Field;
using Application.Services.CommonSrv.StateSrv.Dto;

namespace Application.Services.CommonSrv.CitySrv.Dto
{
    public class CityVDto : Name_FieldDto
    {
        public long StateId { get; set; }
        public StateVDto State { get; set; }
    }
}
