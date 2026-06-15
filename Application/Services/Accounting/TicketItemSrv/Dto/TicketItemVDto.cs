using Application.Common.Dto.Field;
using Application.Services.Dto;
using Application.Services.Filing.FileSrv.Dto;
using System;

namespace Application.Services.Accounting.TicketItemSrv.Dto
{
    public class TicketItemVDto : Id_FieldDto
    {

        public string Body { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public long? FileId { get; set; }
        public long TicketId { get; set; }
        public UserVDto User { get; set; }
        public FileVDto File { get; set; }
    }
}
