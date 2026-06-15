using Application.Common.Dto.Field;

namespace Application.Services.Order.MerchantSrv.Dto
{
    public class MerchantDto : Id_FieldDto
    {
        public long BankId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PrivateKey { get; set; }
        public string TerminalKey { get; set; }
        public string MerchantNo { get; set; }
        public bool Active { get; set; }

    }
}
