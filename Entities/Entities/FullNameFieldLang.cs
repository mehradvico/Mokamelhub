using Entities.Entities.CommonField;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class FullNameFieldLang : FullName_Field
    {
        public long LanguageId { get; set; }
        public Language Language { get; set; }
        public ICollection<GalleryItem> GalleryItems { get; set; }
    }
}
