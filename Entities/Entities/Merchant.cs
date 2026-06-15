using Entities.Entities.CommonField;

namespace Entities.Entities
{
    public class Merchant : Id_Field
    {
        public long BankId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PrivateKey { get; set; }
        public string TerminalKey { get; set; }
        public string MerchantNo { get; set; }
        public bool Active { get; set; }
        public Bank Bank { get; set; }

    }
}
