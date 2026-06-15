using Entities.Entities.CommonField;
using System.Collections.Generic;

namespace Entities.Entities
{

    public class EmailAddress : Id_Field
    {


        public long EmailHostId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string EmailTitle { get; set; }
        public ICollection<Email> Emails { get; set; }
        public EmailHost EmailHost { get; set; }
        public ICollection<EmailSetting> EmailSettings { get; set; }
    }
}
