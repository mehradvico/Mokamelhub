namespace Application.Common.Dto.Field
{
    public class Seo_FieldDto : FullName_FieldDto
    {
        public string SeoH1 { get; set; }
        public string SeoMinDescription { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string SeoPictureAlt { get; set; }
    }
}
