using Application.Common.Dto.Field;
using Application.Services.Dto;
using Application.Services.Filing.FileSrv.Dto;
using Application.Services.ProductSrvs.ProductSrv.Dto;
using Application.Services.Setting.CodeSrv.Dto;
using System;

namespace Application.Services.Accounting.TicketSrv.Dto
{
    public class TicketVDto : Id_FieldDto
    {


        public string Name { get; set; }
        public string Body { get; set; }
        public long UserId { get; set; }
        public long? AdminId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public long? FileId { get; set; }
        public UserVDto User { get; set; }
        public UserVDto Admin { get; set; }
        public FileVDto File { get; set; }
        public CodeVDto Status { get; set; }
        public CodeVDto Importance { get; set; }
        public ProductMinVDto Product { get; set; }
    }
}
