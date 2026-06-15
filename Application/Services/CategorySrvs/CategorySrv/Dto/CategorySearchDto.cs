using Application.Common.Dto.Result;
using Application.Services.CategorySrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.CategorySrv.Dto
{
    public class CategorySearchDto : BaseSearchDto<Category, CategoryChildrenVDto>, ICategorySearchFields
    {
        public CategorySearchDto(CategoryInputDto dto, IQueryable<Category> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.Active = dto.Active;
            this.ParentId = dto.ParentId;
        }
        public bool? Active { get; set; }
        public long? ParentId { get; set; }
    }
}
