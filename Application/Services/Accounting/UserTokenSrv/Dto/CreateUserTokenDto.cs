using Application.Common.Dto.Field;

namespace Application.Services.Accounting.UserTokenSrv.Dto
{
    public class CreateUserTokenDto : Id_FieldDto
    {
        public string Provider { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string DeviceName { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JwtKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string TokenExpires { get; set; }
        public string RefreshExpires { get; set; }
    }
}
