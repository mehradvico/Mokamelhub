using Entities.Entities.CommonField;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class State : Name_Field
    {
        public string EnName { get; set; }
        public long CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<City> Cities { get; set; }
        public ICollection<NameFieldLang> NameFieldLangs { get; set; }
    }
}
