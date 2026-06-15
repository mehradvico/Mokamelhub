

using Application.Common.Dto.Input;
using Application.Services.CategorySrv.Iface;

namespace Application.Services.CategorySrv.Dto
{
    public class CategoryInputDto : BaseInputDto, ICategorySearchFields
    {
        public bool? Active { get; set; }
        public long? ParentId { get; set; }
    }
}
