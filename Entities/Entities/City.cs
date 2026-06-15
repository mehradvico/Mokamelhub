using Entities.Entities.CommonField;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class City : Name_Field
    {
        public long StateId { get; set; }
        public State State { get; set; }
        public ICollection<NameFieldLang> NameFieldLangs { get; set; }
    }
}
