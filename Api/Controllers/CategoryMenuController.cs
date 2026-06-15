using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.CategorySrv.Dto;
using Application.Services.CategorySrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// منو با دسته بندی ها
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryMenuController : ControllerBase
    {
        private ICategoryService categoryService;
        /// <summary>
        /// منو با دسته بندی ها
        /// </summary>
        public CategoryMenuController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id">شناسه دسته بندی</param>
        /// <param name="lang">برچسب زبان</param>
        /// <returns></returns>
        /// 
        [HttpGet("{id}")]
        [CustomOutputCache(CacheTypeEnum.CategoryMenu)]

        [ProducesResponseType(typeof(BaseResultDto<CategoryChildrenMinVDto>), 200)]
        public async Task<IActionResult> Get(long id, string lang = "")
        {

            var category = await categoryService.GetTreeAsync(id, lang);
            return Ok(category);
        }

    }
}
