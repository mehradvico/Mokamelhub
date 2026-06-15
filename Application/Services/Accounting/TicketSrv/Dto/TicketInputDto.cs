

using Application.Common.Dto.Input;
using Application.Common.Enumerable;
using Application.Services.Accounting.TicketSrv.Iface;
using System;

namespace Application.Services.Accounting.TicketSrv.Dto
{
    public class TicketInputDto : BaseInputDto, ITicketSearchFields
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public long? UserId { get; set; }
        public long? AdminId { get; set; }
        public bool AllAdminId { get; set; }
        public TicketStatusEnum? Status { get; set; }
        public TicketImportanceEnum? Importance { get; set; }
    }
}
