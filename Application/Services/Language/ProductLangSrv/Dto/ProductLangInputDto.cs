using Application.Common.Dto.Input;
using Application.Services.Language.ProductLangSrv.Iface;

namespace Application.Services.Language.ProductLangSrv.Dto
{
    public class ProductLangInputDto : BaseInputDto, IProductLangSearchFields
    {
        public long ProductId { get; set; }
    }
}
