namespace Application.Services.Accounting.UserSrv.Dto
{
    public class ChangePasswordDto
    {

        public string Mobile { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
