using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.BrandLangSrv.Dto;
using Application.Services.Language.BrandLangSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت زبان برند ها
    /// </summary>
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandLanguageController : ControllerBase
    {
        private readonly IBrandLangService brandLangService;
        /// <summary>
        /// مدیریت زبان برند ها
        /// </summary>

        public BrandLanguageController(IBrandLangService brandLangService)
        {
            this.brandLangService = brandLangService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<BrandLangDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var item = await brandLangService.FindAsyncDto(id);
            return Ok(item);
        }
        /// <summary>
        /// جستجو
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(BrandLangSearchDto), 200)]
        public IActionResult Get([FromQuery] BrandLangInputDto dto)
        {
            var item = brandLangService.SearchDto(dto);
            return Ok(item);
        }

        /// <summary>
        /// اضافه و ویرایش آیتم
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<BrandLangDto>), 200)]
        public async Task<IActionResult> Post(BrandLangDto dto)
        {
            var item = await brandLangService.InsertAndUpdateAsyncDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// حذف آیتم(ItemId=BrandId)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(OtherLangDeleteDto), 200)]
        public IActionResult Delete(OtherLangDeleteDto dto)
        {
            var item = brandLangService.DeleteDto(dto);
            return Ok(item);
        }

    }
}
