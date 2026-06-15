namespace Application.Common.Dto.Field
{
    public class Seo_Full_FieldDto : Seo_FieldDto
    {
        public string SeoUrlText { get; set; }
        public bool SeoNoIndex { get; set; }
        public bool SeoNoFollow { get; set; }
        public string SeoCanonical { get; set; }
    }
}
