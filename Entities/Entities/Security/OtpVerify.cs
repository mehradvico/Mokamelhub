using Entities.Entities.CommonField;
using System;

namespace Entities.Entities.Security
{
    public class OtpVerify : Id_Field
    {
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool? Verify { get; set; }
        public int TryCount { get; set; }
    }
}
