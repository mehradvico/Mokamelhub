using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.Accounting.TicketSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System;
using System.Linq;

namespace Application.Services.Accounting.TicketSrv.Dto
{
    public class TicketSearchDto : BaseSearchDto<Ticket, TicketVDto>, ITicketSearchFields
    {
        public TicketSearchDto(TicketInputDto dto, IQueryable<Ticket> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.DateFrom = dto.DateFrom;
            this.DateTo = dto.DateTo;
            this.AdminId = dto.AdminId;
            this.Status = dto.Status;
            this.Importance = dto.Importance;
            this.UserId = dto.UserId;
            this.AllAdminId = dto.AllAdminId;

        }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public long? UserId { get; set; }
        public long? AdminId { get; set; }
        public bool AllAdminId { get; set; }
        public TicketStatusEnum? Status { get; set; }
        public TicketImportanceEnum? Importance { get; set; }


    }
}
