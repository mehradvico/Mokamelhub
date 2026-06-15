namespace Application.Services.Accounting.UserSrv.Dto
{
    public class ResetPasswordDto : ForgetPasswordDto
    {

        public string Code { get; set; }

        public string Password { get; set; }
    }
}
