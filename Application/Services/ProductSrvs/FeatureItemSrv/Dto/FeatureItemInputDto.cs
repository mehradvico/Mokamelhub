using Application.Common.Dto.Input;
using Application.Services.ProductSrvs.FeatureItemSrv.Iface;

namespace Application.Services.ProductSrvs.FeatureItemSrv.Dto
{
    public class FeatureItemInputDto : BaseInputDto, IFeatureItemSearchFields
    {
        public long FeatureId { get; set; }

    }
}
