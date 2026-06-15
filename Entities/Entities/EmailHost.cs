
using Entities.Entities.CommonField;
using System.Collections.Generic;

namespace Entities.Entities
{

    public class EmailHost : Name_Field
    {

        public string Pop3 { get; set; }
        public string Smtp { get; set; }
        public int Pop3Port { get; set; }
        public int SmtpPort { get; set; }
        public bool Ssl { get; set; }
        public bool Active { get; set; }
        public string Body { get; set; }

        public ICollection<EmailAddress> EmailAddress { get; set; }
    }
}
