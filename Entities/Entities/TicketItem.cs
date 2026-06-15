using Entities.Entities.CommonField;
using Entities.Entities.Security;
using System;

namespace Entities.Entities
{
    public class TicketItem : Id_Field
    {
        public string Body { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public long? FileId { get; set; }
        public long TicketId { get; set; }
        public bool Deleted { get; set; }
        public User User { get; set; }
        public File File { get; set; }
        public Ticket Ticket { get; set; }

    }
}
