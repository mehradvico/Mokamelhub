using Entities.Entities.CommonField;
using Entities.Entities.Security;
using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class ContactUs : Id_Field
    {
        public long? UserId { get; set; }
        public long? ProductIdItemId { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public long ContactUsGroupId { get; set; }
        public string Answer { get; set; }
        public long? FileId { get; set; }
        public File File { get; set; }
        public User User { get; set; }
        public ContactUsGroup ContactUsGroup { get; set; }
        public ProductItem ProductItem { get; set; }
        public ICollection<ContactUsItem> ContactUsItems { get; set; }

    }
}
