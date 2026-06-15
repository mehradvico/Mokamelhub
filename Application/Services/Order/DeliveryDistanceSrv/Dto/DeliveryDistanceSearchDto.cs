using Application.Common.Dto.Result;
using Application.Services.Order.DeliveryDistanceSrv.iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Order.DeliveryDistanceSrv.Dto
{
    public class DeliveryDistanceSearchDto : BaseSearchDto<DeliveryDistance, DeliveryDistanceVDto>, IDeliveryDistanceSearchFields
    {
        public DeliveryDistanceSearchDto(DeliveryDistanceInputDto dto, IQueryable<DeliveryDistance> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.DeliveryId = dto.DeliveryId;
        }
        public long DeliveryId { get; set; }
    }
}
