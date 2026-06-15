using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.CodeGroupLangSrv.Dto;
using Application.Services.Language.CodeGroupLangSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت زبان گروه کد ها
    /// </summary>

    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class CodeGroupLanguageController : ControllerBase
    {
        private readonly ICodeGroupLangService codeGroupLangService;
        /// <summary>
        /// مدیریت زبان گروه کد ها
        /// </summary>
        /// 

        public CodeGroupLanguageController(ICodeGroupLangService codeGroupLangService)
        {
            this.codeGroupLangService = codeGroupLangService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<CodeGroupLangDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var item = await codeGroupLangService.FindAsyncDto(id);
            return Ok(item);
        }
        /// <summary>
        /// جستجو
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CodeGroupLangSearchDto), 200)]
        public IActionResult Get([FromQuery] CodeGroupLangInputDto dto)
        {
            var item = codeGroupLangService.SearchDto(dto);
            return Ok(item);
        }

        /// <summary>
        /// اضافه و ویرایش آیتم
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<CodeGroupLangDto>), 200)]
        public async Task<IActionResult> Post(CodeGroupLangDto dto)
        {
            var item = await codeGroupLangService.InsertAndUpdateAsyncDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// حذف آیتم(ItemId=CodeGroupId)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(OtherLangDeleteDto), 200)]
        public IActionResult Delete(OtherLangDeleteDto dto)
        {
            var item = codeGroupLangService.DeleteDto(dto);
            return Ok(item);
        }

    }
}
