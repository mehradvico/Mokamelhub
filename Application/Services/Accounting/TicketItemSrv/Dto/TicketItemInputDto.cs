

using Application.Common.Dto.Input;
using Application.Services.Accounting.TicketItemSrv.Iface;

namespace Application.Services.Accounting.TicketItemSrv.Dto
{
    public class TicketItemInputDto : BaseInputDto, ITicketItemSearchFields
    {
        public long TicketId { get; set; }

    }
}
