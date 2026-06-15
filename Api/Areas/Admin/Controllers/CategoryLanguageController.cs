using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.CategoryLangSrv.Dto;
using Application.Services.Language.CategoryLangSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت زبان دسته بندی ها
    /// </summary>
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryLanguageController : ControllerBase
    {
        private readonly ICategoryLangService categoryLangService;
        /// <summary>
        /// مدیریت زبان فروشگاه ها
        /// </summary>

        public CategoryLanguageController(ICategoryLangService categoryLangService)
        {
            this.categoryLangService = categoryLangService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<CategoryLangDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var item = await categoryLangService.FindAsyncDto(id);
            return Ok(item);
        }
        /// <summary>
        /// جستجو
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CategoryLangSearchDto), 200)]
        public IActionResult Get([FromQuery] CategoryLangInputDto dto)
        {
            var item = categoryLangService.SearchDto(dto);
            return Ok(item);
        }

        /// <summary>
        /// اضافه و ویرایش آیتم
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<CategoryLangDto>), 200)]
        public async Task<IActionResult> Post(CategoryLangDto dto)
        {
            var item = await categoryLangService.InsertAndUpdateAsyncDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// حذف آیتم(ItemId=CategoryId)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(OtherLangDeleteDto), 200)]
        public IActionResult Delete(OtherLangDeleteDto dto)
        {
            var item = categoryLangService.DeleteDto(dto);
            return Ok(item);
        }

    }
}
