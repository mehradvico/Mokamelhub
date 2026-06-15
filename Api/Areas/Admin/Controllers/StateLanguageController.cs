using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.StateLangSrv.Dto;
using Application.Services.Language.StateLangSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت زبان استان ها
    /// </summary>
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class StateLanguageController : ControllerBase
    {
        private readonly IStateLangService stateLangService;
        /// <summary>
        /// مدیریت زبان استان ها
        /// </summary>
        ///        
        public StateLanguageController(IStateLangService stateLangService)
        {
            this.stateLangService = stateLangService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<StateLangDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var item = await stateLangService.FindAsyncDto(id);
            return Ok(item);
        }
        /// <summary>
        /// جستجو
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(StateLangSearchDto), 200)]
        public IActionResult Get([FromQuery] StateLangInputDto dto)
        {
            var item = stateLangService.SearchDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// اضافه و ویرایش آیتم
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<StateLangDto>), 200)]
        public async Task<IActionResult> Post(StateLangDto dto)
        {
            var item = await stateLangService.InsertAndUpdateAsyncDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// حذف آیتم(ItemId=StateId)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(OtherLangDeleteDto), 200)]
        public IActionResult Delete(OtherLangDeleteDto dto)
        {
            var item = stateLangService.DeleteDto(dto);
            return Ok(item);
        }
    }
}
