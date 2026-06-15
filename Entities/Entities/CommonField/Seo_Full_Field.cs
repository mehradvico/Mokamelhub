namespace Entities.Entities.CommonField
{
    public class Seo_Full_Field : Seo_Field
    {

        public string SeoUrlText { get; set; }
        public bool SeoNoIndex { get; set; }
        public bool SeoNoFollow { get; set; }
        public string SeoCanonical { get; set; }
    }
}
