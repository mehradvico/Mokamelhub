using Application.Common.Dto.Result;
using Application.Services.Accounting.TicketItemSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Accounting.TicketItemSrv.Dto
{
    public class TicketItemSearchDto : BaseSearchDto<TicketItem, TicketItemVDto>, ITicketItemSearchFields
    {
        public TicketItemSearchDto(TicketItemInputDto dto, IQueryable<TicketItem> list, IMapper mapper) : base(dto, list, mapper)
        {

            this.TicketId = dto.TicketId;
        }

        public long TicketId { get; set; }

    }
}
