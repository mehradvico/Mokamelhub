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
    public class CategoryController : ControllerBase
    {
        private ICategoryService categoryService;
        /// <summary>
        /// مرتبط با پست ها
        /// </summary>
        public CategoryController(ICategoryService categoryService)
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
        [CustomOutputCache(CacheTypeEnum.CategoryOne)]
        [ProducesResponseType(typeof(BaseResultDto<CategoryChildrenVDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var category = await categoryService.FindChildrenVDtoAsync(id, active: true);
            return Ok(category);
        }
        /// <summary>
        /// اطلاعات آیتم با برچسب
        /// </summary>
        /// <param name="label">برچسب دسته بندی</param>
        /// <returns></returns>
        /// 
        [HttpGet("label/{label}")]
        [CustomOutputCache(CacheTypeEnum.CategorySearch)]
        [ProducesResponseType(typeof(BaseResultDto<CategoryChildrenVDto>), 200)]
        public async Task<IActionResult> Get(string label)
        {
            var category = await categoryService.FindChildrenVDtoAsync(label, active: true);
            return Ok(category);
        }

        /// <summary>
        /// جستجو
        /// </summary>
        [HttpGet]
        //[OutputCache(Duration = 5000)]

        [ProducesResponseType(typeof(CategorySearchDto), 200)]
        public IActionResult Get([FromQuery] CategoryInputDto dto)
        {
            var post = categoryService.Search(dto);
            return Ok(post);
        }
    }
}
