using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.VarietyLangSrv.Dto;
using Application.Services.Language.VarietyLangSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت زبان تنوع ها
    /// </summary>
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class VarietyLanguageController : ControllerBase
    {
        private readonly IVarietyLangService varietyLangService;
        /// <summary>
        /// مدیریت زبان تنوع ها
        /// </summary>

        public VarietyLanguageController(IVarietyLangService varietyLangService)
        {
            this.varietyLangService = varietyLangService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<VarietyLangDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var item = await varietyLangService.FindAsyncDto(id);
            return Ok(item);
        }
        /// <summary>
        /// جستجو
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(VarietyLangSearchDto), 200)]
        public IActionResult Get([FromQuery] VarietyLangInputDto dto)
        {
            var item = varietyLangService.SearchDto(dto);
            return Ok(item);
        }

        /// <summary>
        /// اضافه و ویرایش آیتم
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<VarietyLangDto>), 200)]
        public async Task<IActionResult> Post(VarietyLangDto dto)
        {
            var item = await varietyLangService.InsertAndUpdateAsyncDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// حذف آیتم(ItemId=VrietyId)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(OtherLangDeleteDto), 200)]
        public IActionResult Delete(OtherLangDeleteDto dto)
        {
            var item = varietyLangService.DeleteDto(dto);
            return Ok(item);
        }

    }
}
