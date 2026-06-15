using Application.Common.Dto.Field;
using Application.Services.Dto;
using Application.Services.ProductSrvs.ProductSrv.Dto;
using System;

namespace Application.Services.Accounting.UserProductSrv.Dto
{
    public class UserProductVDto : Id_FieldDto
    {
        public long ProductId { get; set; }
        public long UserId { get; set; }
        public long Price { get; set; }
        public string SpotPlayerToken { get; set; }
        public string SpotPlayerUrl { get; set; }
        public string SpotPlayerId { get; set; }
        public DateTime TicketExpireDate { get; set; }
        public bool HasTicket { get; set; }
        public ProductVDto Product { get; set; }
        public UserVDto User { get; set; }

        public string ProductName { get; set; }
        public string MerchantTrackNo { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
