using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.CategoryVariety.Dto;
using Application.Services.ProductSrvs.CategoryVariety.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت تنوع های دسته بندی
    /// </summary>
    [Area("Admin")]
    [Route("api/[area]/[Controller]")]
    [ApiController]
    [Authorize]
    public class CategoryVariety : ControllerBase
    {
        private ICategoryVarietyService categoryVarietyService;
        /// <summary>
        /// مدیریت تنوع های دسته بندی
        /// </summary>
        public CategoryVariety(ICategoryVarietyService categoryVarietyService)
        {
            this.categoryVarietyService = categoryVarietyService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>  
        [HttpGet]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public async Task<IActionResult> Get(long categoryId)
        {
            var dto = await categoryVarietyService.FindAsyncDto(categoryId);
            return Ok(dto);
        }
        /// <summary>
        /// اضافه و ویرایش تنوع دسته بندی
        /// </summary>  
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public async Task<IActionResult> Post(CategoryVarietyDto dto)
        {
            var result = await categoryVarietyService.InsertAndUpdateAsyncDto(dto);
            return Ok(result);
        }
    }
}
