using Entities.Entities.CommonField;
using System;

namespace Entities.Entities
{
    public class Sms : Id_Field
    {
        public string Sender { get; set; }
        public string Receptor { get; set; }
        public bool IsSend { get; set; }
        public string Token1 { get; set; }
        public string Token2 { get; set; }
        public string Token3 { get; set; }
        public string Token4 { get; set; }
        public string Token5 { get; set; }
        public string Body { get; set; }
        public bool? Status { get; set; }
        public string StatusText { get; set; }
        public double? Cost { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? SentDate { get; set; }

        public long SmsTypeId { get; set; }

        public MessageType SmsType { get; set; }
    }
}
