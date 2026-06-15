using Application.Common.Dto.Input;
using Application.Services.CommonSrv.CitySrv.Iface;

namespace Application.Services.CommonSrv.CitySrv.Dto
{
    public class CityInputDto : BaseInputDto, ICitySearchFields
    {
        public long StateId { get; set; }
    }
}
