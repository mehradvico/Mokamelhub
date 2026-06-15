using Application.Common.Dto.Input;
using Application.Services.ProductSrvs.FeatureSrv.Iface;

namespace Application.Services.ProductSrvs.VarietyItemSrv.Dto
{
    public class VarietyItemInputDto : BaseInputDto, IVarietyItemSearchFields
    {
        public long VarietyId { get; set; }

    }
}
