using Entities.Entities.CommonField;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class NameFieldLang : Name_Field
    {
        public long LanguageId { get; set; }
        public Language Language { get; set; }
        public ICollection<Code> Codes { get; set; }
        public ICollection<CodeGroup> CodeGroups { get; set; }
        public ICollection<Feature> Features { get; set; }
        public ICollection<FeatureItem> FeatureItems { get; set; }
        public ICollection<ProductFeatureValue> ProductFeatureValues { get; set; }
        public ICollection<Variety> Varieties { get; set; }
        public ICollection<City> Cities { get; set; }
        public ICollection<State> States { get; set; }



    }
}
