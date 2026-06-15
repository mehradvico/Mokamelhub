using Application.Common.Dto.Field;
using System.Globalization;

namespace Application.Services.CommonSrv.StateSrv.Dto
{
    public class StateDto : Name_FieldDto
    {
        public string EnName { get; set; }
        public long CountryId { get; set; }
    }
}
