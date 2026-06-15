using Application.Common.Dto.Field;
using System;

namespace Application.Services.Accounting.UserProductSrv.Dto
{
    public class UserProductDto : Id_FieldDto
    {
        public long ProductId { get; set; }
        public long UserId { get; set; }
        public double Price { get; set; }
        public string SpotPlayerToken { get; set; }
        public string SpotPlayerUrl { get; set; }
        public string SpotPlayerId { get; set; }
        public DateTime CreateDate { get; set; }


    }
}
