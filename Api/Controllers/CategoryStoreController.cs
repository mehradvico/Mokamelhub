using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.CategorySrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// دسته بندی های فروشگاه
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryStoreController : ControllerBase
    {
        private ICategoryService _categoryService;
        /// <summary>
        /// دسته بندی های فروشگاه
        /// </summary>
        public CategoryStoreController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="storeId">فروشگاه</param>
        /// <returns></returns>
        /// 
        [HttpGet("id/{storeId}")]
        [CustomOutputCache(CacheTypeEnum.CategoryStore)]
        [ProducesResponseType(typeof(BaseResultDto<long>), 200)]
        public async Task<IActionResult> Get(long storeId)
        {
            var result = await _categoryService.CategoryStore(storeId);
            return Ok(result);
        }
    }
}
