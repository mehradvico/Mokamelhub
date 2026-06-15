using Application.Common.Dto.Input;
using Application.Services.Language.FeatureLangSrv.Iface;

namespace Application.Services.Language.FeatureLangSrv.Dto
{
    public class FeatureLangInputDto : BaseInputDto, IFeatureLangSearchFields
    {
        public long FeatureId { get; set; }
    }
}
