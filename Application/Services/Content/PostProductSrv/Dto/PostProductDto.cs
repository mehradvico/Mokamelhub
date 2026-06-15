using Application.Common.Dto.Field;
using Application.Services.ProductSrvs.ProductSrv.Dto;
using System.Collections.Generic;

namespace Application.Services.Content.PostProductSrv.Dto
{
    public class PostProductDto : Name_FieldDto
    {

        public List<ProductVDto> Products { get; set; } = new List<ProductVDto>();
    }
}
