using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.Order.DeliverySrv.iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Order.DeliverySrv.Dto
{
    public class DeliverySearchDto : BaseSearchDto<Delivery, DeliveryVDto>, IDeliverySearchFields
    {
        public DeliverySearchDto(DeliveryInputDto dto, IQueryable<Delivery> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.DeliveryTypeEnum = dto.DeliveryTypeEnum;
            this.StoreId = dto.StoreId;
        }
        public DeliveryTypeEnum? DeliveryTypeEnum { get; set; }
        public long? StoreId { get; set; }

    }
}
