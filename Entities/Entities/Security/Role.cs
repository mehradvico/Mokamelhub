using Entities.Entities.CommonField;
using System.Collections.Generic;

namespace Entities.Entities.Security
{
    public class Role : Id_Field
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
