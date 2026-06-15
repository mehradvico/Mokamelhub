namespace Application.Services.Accounting.OtpVerifySrv.Dto
{
    public class OtpVerifyResultVDto
    {
        public string Mobile { get; set; }
        public string Code { get; set; }
        public bool HasAccount { get; set; }
        public bool TwoFactorEnabled { get; set; }

    }
}
