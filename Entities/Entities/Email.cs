using Entities.Entities.CommonField;
using System;
namespace Entities.Entities
{


    public class Email : Id_Field
    {
        public string Title { get; set; }
        public string Sender { get; set; }
        public string Receptor { get; set; }
        public bool IsSend { get; set; }
        public string Body { get; set; }
        public bool? Status { get; set; }
        public string StatusText { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? SentDate { get; set; }
        public long EmailTypeId { get; set; }
        public MessageType EmailType { get; set; }
    }
}
