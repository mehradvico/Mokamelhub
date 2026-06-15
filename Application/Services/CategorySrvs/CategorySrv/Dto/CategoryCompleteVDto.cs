using System.Collections.Generic;

namespace Application.Services.CategorySrv.Dto
{
    public class CategoryCompleteVDto : CategoryVDto
    {

        public List<CategoryChildrenVDto> Children { get; set; }
        public CategoryParentVDto Parent { get; set; }
    }
}
