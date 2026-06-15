using Application.Common.Service;
using Application.Services.Order.DeliveryDistanceSrv.Dto;
using Application.Services.Order.DeliveryDistanceSrv.iface;
using Application.Services.Order.DeliverySrv.Dto;
using AutoMapper;
using Entities.Entities;
using Persistence.Interface;
using System.Linq;

namespace Application.Services.Content.DeliveryDistanceSrv
{
    public class DeliveryDistanceService : CommonSrv<DeliveryDistance, DeliveryDistanceDto>, IDeliveryDistanceService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public DeliveryDistanceService(IDataBaseContext _context, IMapper mapper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }


        public DeliveryDistanceSearchDto Search(DeliveryDistanceInputDto searchDto)
        {
            var query = _context.DeliveryDistances.AsQueryable();
            query = query.Where(s => s.DeliveryId == searchDto.DeliveryId);
            if (searchDto.SortBy != Common.Enumerable.SortEnum.Default)
            {
                switch (searchDto.SortBy)
                {
                    case Common.Enumerable.SortEnum.Default:
                        {
                            query = query.OrderByDescending(s => s.Id);
                            break;
                        }
                    case Common.Enumerable.SortEnum.New:
                        {
                            query = query.OrderByDescending(s => s.Id);
                            break;
                        }
                    case Common.Enumerable.SortEnum.Old:
                        {
                            query = query.OrderBy(s => s.Id);
                            break;
                        }
                    default:
                        break;
                }
            }

            return new DeliveryDistanceSearchDto(searchDto, query, mapper);
        }
    }
}
