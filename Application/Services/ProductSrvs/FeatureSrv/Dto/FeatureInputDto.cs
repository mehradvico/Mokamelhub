using Application.Common.Dto.Input;
using Application.Common.Enumerable;
using Application.Services.ProductSrvs.FeatureSrv.Iface;

namespace Application.Services.ProductSrvs.FeatureSrv.Dto
{
    public class FeatureInputDto : BaseInputDto, IFeatureSearchFields
    {
        public long? CategoryId { get; set; }
        public string CategoryLabel { get; set; }
        public bool? IsGroup { get; set; }
        public bool GetChildren { get; set; }
        public bool? IsHide { get; set; }

        public bool? InSearch { get; set; }
        public FeatureTypeEnum? FeatureTypeEnum { get; set; }

    }
}
