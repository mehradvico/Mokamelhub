using Application.Common.Dto.Field;
using Application.Services.CommonSrv.StateSrv.Dto;
using System.Runtime.Serialization;

namespace Application.Services.CommonSrv.CitySrv.Dto
{
    public class CityDto : Name_FieldDto
    {
        public int StateId { get; set; }
        [IgnoreDataMember]
        public StateDto State { get; set; }
    }
}
