using Entities.Entities.CommonField;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class Neighborhood : Name_Field
    {
        public int RegionNumber { get; set; }
        public long CityId { get; set; }
        public City City { get; set; }

        public ICollection<NameFieldLang> NameFieldLangs { get; set; }


    }
}
