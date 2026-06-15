using Application.Common.Dto.Input;
using Application.Common.Enumerable;
using Application.Services.ProductSrvs.ProductFeatureValueSrv.Dto;
using Application.Services.ProductSrvs.ProductFeatureValueSrv.Iface;
using System.Collections.Generic;

namespace Application.Services.ProductSrvs.FeatureItemSrv.Dto
{
    public class ProductFeatureValueInputDto : BaseInputDto, IProductFeatureValueSearchFields
    {
        public long ProductId { get; set; }
        public FeatureTypeEnum? FeatureTypeEnum { get; set; }
        public bool? Hide { get; set; }
        public int TotalCount { get; set; }

        public List<ProductFeatureVDto> List { get; set; }
    }
}
