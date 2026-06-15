using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.CityLangSrv.Dto;
using Application.Services.Language.CityLangSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت زبان شهر ها
    /// </summary>
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class CityLanguageController : ControllerBase
    {
        private readonly ICityLangService cityLangService;
        /// <summary>
        /// مدیریت زبان شهر ها
        /// </summary>
        public CityLanguageController(ICityLangService cityLangService)
        {
            this.cityLangService = cityLangService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<CityLangDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var item = await cityLangService.FindAsyncDto(id);
            return Ok(item);
        }
        /// <summary>
        /// جستجو
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CityLangSearchDto), 200)]
        public IActionResult Get([FromQuery] CityLangInputDto dto)
        {
            var item = cityLangService.SearchDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// اضافه و ویرایش آیتم
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<CityLangDto>), 200)]
        public async Task<IActionResult> Post(CityLangDto dto)
        {
            var item = await cityLangService.InsertAndUpdateAsyncDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// حذف آیتم(ItemId=CityId)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(OtherLangDeleteDto), 200)]
        public IActionResult Delete(OtherLangDeleteDto dto)
        {
            var item = cityLangService.DeleteDto(dto);
            return Ok(item);
        }
    }
}
