using Application.Common.Dto.Input;
using Application.Services.CommonSrv.NeighborhoodSrv.Iface;

namespace Application.Services.CommonSrv.NeighborhoodSrv.Dto
{
    public class NeighborhoodInputDto : BaseInputDto, INeighborhoodSearchFields
    {
        public long? CityId { get; set; }
        public long? StateId { get; set; }
    }
}
