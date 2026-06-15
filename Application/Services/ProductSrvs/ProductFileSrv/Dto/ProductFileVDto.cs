using Application.Common.Dto.Field;
using Application.Services.Filing.FileSrv.Dto;
using System.Collections.Generic;

namespace Application.Services.ProductSrvs.ProductFileSrv.Dto
{
    public class ProductFileVDto : Name_FieldDto
    {
        public long ProductId { get; set; }
        public long? FileId { get; set; }
        public long? ParentId { get; set; }
        public string Label { get; set; }
        public int Priority { get; set; }
        public bool Protected { get; set; }
        public FileVDto File { get; set; }
        public List<ProductFileVDto> Children { get; set; }
    }
}
