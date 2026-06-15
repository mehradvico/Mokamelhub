using Application.Common.Dto.Input;
using Application.Services.Content.StaticPageSrv.Iface;

namespace Application.Services.Content.StaticPageSrv.Dto
{
    public class StaticPageInputDto : BaseInputDto, IStaticPageSearchFields
    {
        public string Label { get; set; }
    }
}
