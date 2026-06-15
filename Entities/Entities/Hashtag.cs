using Entities.Entities.CommonField;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class Hashtag : Name_Field
    {
        public ICollection<Post> posts { get; set; }
    }
}
