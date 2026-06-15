using Application.Common.Interface;
using Application.Services.CommonSrv.NeighborhoodSrv.Dto;
using Entities.Entities;

namespace Application.Services.CommonSrv.NeighborhoodSrv.Iface
{
    public interface INeighborhoodService : ICommonSrv<Neighborhood, NeighborhoodDto>
    {
        NeighborhoodSearchDto Search(NeighborhoodInputDto inputdto);
    }
}
