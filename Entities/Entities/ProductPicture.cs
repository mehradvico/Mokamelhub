using Entities.Entities.CommonField;

namespace Entities.Entities
{
    public class ProductPicture : Id_Field
    {
        public long ProductId { get; set; }
        public long PictureId { get; set; }
        public string Label { get; set; }
        public Picture Picture { get; set; }
        public Product Product { get; set; }
    }
}
