using Entities.Entities.CommonField;

namespace Entities.Entities
{
    public class ProductRelate : Id_Field
    {
        public string Label { get; set; }
        public long ProductId { get; set; }
        public long RelatedProductId { get; set; }
        public Product Product { get; set; }
        public Product RelatedProduct { get; set; }
    }
}
