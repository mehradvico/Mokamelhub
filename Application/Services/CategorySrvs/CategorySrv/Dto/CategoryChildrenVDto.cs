using System.Collections.Generic;

namespace Application.Services.CategorySrv.Dto
{
    public class CategoryChildrenVDto : CategoryVDto
    {

        public List<CategoryChildrenVDto> Children { get; set; } = new List<CategoryChildrenVDto>();
    }

}
