using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.CategorySrv.Dto;
using Application.Services.CategorySrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با دسته بندی ها
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategorySiteMapController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        /// <summary>
        /// مرتبط با سایت مپ دسته بندی ها
        /// </summary>
        public CategorySiteMapController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        /// <summary>
        /// سایت مپ دسته بندی ها
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("{label}")]
        [CustomOutputCache(CacheTypeEnum.CategorySiteMap)]
        [ProducesResponseType(typeof(BaseResultDto<CategorySiteMapDto>), 200)]
        public IActionResult Get(string label)
        {
            var list = _categoryService.GetSiteMap(label);
            return Ok(list);
        }

    }
}
