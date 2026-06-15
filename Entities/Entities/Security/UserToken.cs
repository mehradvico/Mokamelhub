using Entities.Entities.CommonField;
using System;

namespace Entities.Entities.Security
{
    public class UserToken : Id_Field
    {
        public string Provider { get; set; }
        public string TokenHash { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime TokenExp { get; set; }
        public string RefreshTokenHash { get; set; }
        public DateTime RefreshTokenExp { get; set; }
        public string DeviceName { get; set; }
        public bool Deleted { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
