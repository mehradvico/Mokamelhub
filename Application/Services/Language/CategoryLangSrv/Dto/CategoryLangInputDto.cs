using Application.Common.Dto.Input;
using Application.Services.Language.CategoryLangSrv.Iface;

namespace Application.Services.Language.CategoryLangSrv.Dto
{
    public class CategoryLangInputDto : BaseInputDto, ICategoryLangSearchFields
    {
        public long CategoryId { get; set; }
    }
}
