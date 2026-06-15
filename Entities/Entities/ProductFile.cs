using Entities.Entities.CommonField;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class ProductFile : Name_Field
    {
        public long ProductId { get; set; }
        public long? FileId { get; set; }
        public long? ParentId { get; set; }
        public string Label { get; set; }
        public int Priority { get; set; }
        public bool Protected { get; set; }
        public bool Deleted { get; set; }
        public Product Product { get; set; }
        public File File { get; set; }
        public ICollection<ProductFile> Children { get; set; }
        public ProductFile Parent { get; set; }
    }
}
