using Application.Common.Dto.Field;

namespace Application.Services.Accounting.OtpVerifySrv.Dto
{
    public class OtpVerifyDto : Id_FieldDto
    {
        public string Mobile { get; set; }
        public string Code { get; set; }
        public bool? Verify { get; set; }

    }
}
