using System.Collections.Generic;

namespace Application.Services.ProductSrvs.FeatureItemSrv.Dto
{
    public class ProductFeatureValueAddDto
    {
        public long ProductId { get; set; }
        public List<ProductFeatureValueDto> ProductFeatures { get; set; }

    }
}
