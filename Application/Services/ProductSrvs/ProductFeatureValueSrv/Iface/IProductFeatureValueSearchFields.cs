using Application.Common.Enumerable;
using Application.Services.ProductSrvs.ProductFeatureValueSrv.Dto;
using System.Collections.Generic;

namespace Application.Services.ProductSrvs.ProductFeatureValueSrv.Iface
{
    public interface IProductFeatureValueSearchFields
    {
        public long ProductId { get; set; }
        public FeatureTypeEnum? FeatureTypeEnum { get; set; }
        public bool? Hide { get; set; }
        public int TotalCount { get; set; }

        public List<ProductFeatureVDto> List { get; set; }
    }
}
