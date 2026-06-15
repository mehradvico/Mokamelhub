using Application.Common.Dto.Field;
using System;

namespace Application.Services.Accounting.UserTokenSrv.Dto
{
    public class UserTokenDto : Id_FieldDto
    {
        public string Provider { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string DeviceName { get; set; }
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime TokenExp { get; set; }
        public DateTime RefreshTokenExp { get; set; }
    }
}
