using Entities.Entities.CommonField;
using System;

namespace Entities.Entities
{
    public class Newsletter : Id_Field
    {
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
