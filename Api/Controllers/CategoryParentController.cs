using Application.Common.Dto.Result;
using Application.Services.CategorySrv.Dto;
using Application.Services.CategorySrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با پدران دسته بندی ها
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryParentController : ControllerBase
    {
        private ICategoryService categoryService;
        /// <summary>
        /// مرتبط با پدران دسته بندی ها
        /// </summary>
        public CategoryParentController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id">شناسه دسته بندی</param>
        /// <returns></returns>
        /// 
        [HttpGet("{id}")]
        //[OutputCache(Duration = 5000)]
        [ProducesResponseType(typeof(BaseResultDto<List<SearchCategoryDto>>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var category = await categoryService.GetAllActiveParents(id);
            return Ok(category);
        }
    }
}
