using Application.Common.Dto.Input;
using Application.Services.Language.CityLangSrv.Iface;

namespace Application.Services.Language.CityLangSrv.Dto
{
    public class CityLangInputDto : BaseInputDto, ISatetLangSearchFields
    {
        public long CityId { get; set; }
    }
}
