using Entities.Entities.CommonField;

namespace Entities.Entities
{
    public class SmsSetting : Id_Field
    {
        public long SmsNumberId { get; set; }
        public long SmsTypeId { get; set; }

        public SmsNumber SmsNumber { get; set; }
        public MessageType SmsType { get; set; }
    }
}
