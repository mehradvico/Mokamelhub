using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.CodeLangSrv.Dto;
using Application.Services.Language.CodeLangSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت زبان کد ها
    /// </summary>
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class CodeLanguageController : ControllerBase
    {
        private readonly ICodeLangService codeLangService;
        /// <summary>
        /// مدیریت زبان کد ها
        /// </summary>
        /// 

        public CodeLanguageController(ICodeLangService codeLangService)
        {
            this.codeLangService = codeLangService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<CodeLangDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var item = await codeLangService.FindAsyncDto(id);
            return Ok(item);
        }
        /// <summary>
        /// جستجو
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CodeLangSearchDto), 200)]
        public IActionResult Get([FromQuery] CodeLangInputDto dto)
        {
            var item = codeLangService.SearchDto(dto);
            return Ok(item);
        }

        /// <summary>
        /// اضافه و ویرایش آیتم
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<CodeLangDto>), 200)]
        public async Task<IActionResult> Post(CodeLangDto dto)
        {
            var item = await codeLangService.InsertAndUpdateDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// حذف آیتم(ItemId=CodeId)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(OtherLangDeleteDto), 200)]
        public IActionResult Delete(OtherLangDeleteDto dto)
        {
            var item = codeLangService.DeleteDto(dto);
            return Ok(item);
        }

    }
}
