using Entities.Entities.CommonField;

namespace Entities.Entities
{
    public class SmsNumber : Id_Field
    {
        public long SmsProviderId { get; set; }
        public string Number { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }
        public SmsProvider SmsProvider { get; set; }
    }
}
