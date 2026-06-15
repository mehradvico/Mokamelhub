using Entities.Entities.CommonField;
using System.Collections.Generic;

namespace Entities.Entities.Security
{
    public class Permission : Id_Field
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool IsMenu { get; set; }
        public int Priority { get; set; }
        public long? ParentId { get; set; }
        public bool Deleted { get; set; }
        public Permission Parent { get; set; }
        public ICollection<Permission> Children { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
