using Entities.Entities.CommonField;

namespace Application.Services.CommonSrv.NeighborhoodSrv.Dto
{
    public class NeighborhoodDto : Name_Field
    {
        public int RegionNumber { get; set; }
        public long CityId { get; set; }

    }
}
