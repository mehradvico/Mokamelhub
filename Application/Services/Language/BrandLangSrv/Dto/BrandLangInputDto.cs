using Application.Common.Dto.Input;
using Application.Services.Language.BrandLangSrv.Iface;

namespace Application.Services.Language.BrandLangSrv.Dto
{
    public class BrandLangInputDto : BaseInputDto, IBrandLangSearchFields
    {
        public long BrandId { get; set; }
    }
}
