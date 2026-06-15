using Application.Common.Dto.Input;
using Application.Services.Language.VarietyLangSrv.Iface;

namespace Application.Services.Language.VarietyLangSrv.Dto
{
    public class VarietyLangInputDto : BaseInputDto, IVarietyLangSearchFields
    {
        public long VarietyId { get; set; }
    }
}
