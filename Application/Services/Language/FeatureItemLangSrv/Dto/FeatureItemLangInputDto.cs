using Application.Common.Dto.Input;
using Application.Services.Language.FeatureItemLangSrv.Iface;

namespace Application.Services.Language.FeatureItemLangSrv.Dto
{
    public class FeatureItemLangInputDto : BaseInputDto, IFeatureItemLangSearchFields
    {
        public long FeatureItemId { get; set; }
    }
}
