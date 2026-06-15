using Application.Common.Service;
using Application.Services.CommonSrv.NeighborhoodSrv.Dto;
using Application.Services.CommonSrv.NeighborhoodSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Linq;

namespace Application.Services.CommonSrv.NeighborhoodSrv
{
    public class NeighborhoodService : CommonSrv<Neighborhood, NeighborhoodDto>, INeighborhoodService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public NeighborhoodService(IDataBaseContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }



        public NeighborhoodSearchDto Search(NeighborhoodInputDto inputdto)
        {
            var query = _context.Neighborhoods.Include(s => s.City).ThenInclude(s => s.State).AsQueryable();
            if (inputdto.CityId.HasValue)
            {
                query = query.Where(s => s.CityId == inputdto.CityId.Value);
            }
            if (inputdto.StateId.HasValue)
            {
                query = query.Where(s => s.City.StateId == inputdto.StateId.Value);
            }
            return new NeighborhoodSearchDto(inputdto, query, _mapper);
        }
    }
}
