
using Entities.Entities.CommonField;

namespace Entities.Entities
{

    public class EmailSetting : Id_Field
    {
        public long EmailAddressId { get; set; }
        public long EmailTypeId { get; set; }

        public EmailAddress EmailAddress { get; set; }
        public MessageType EmailType { get; set; }
    }
}
