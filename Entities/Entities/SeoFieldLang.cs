using Entities.Entities.CommonField;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class SeoFieldLang : Seo_Field
    {
        public long LanguageId { get; set; }
        public Language Language { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Brand> brands { get; set; }
        public ICollection<Gallery> Galleries { get; set; }




    }
}
