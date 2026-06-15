using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.PostLangSrv.Dto;
using Application.Services.Language.PostLangSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت زبان پست ها
    /// </summary>
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class PostLanguageController : ControllerBase
    {
        private readonly IPostLangService postLangService;
        /// <summary>
        /// مدیریت زبان پست ها
        /// </summary>
        /// <param name="postLangService"></param>
        public PostLanguageController(IPostLangService postLangService)
        {
            this.postLangService = postLangService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<PostLangDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var item = await postLangService.FindAsyncDto(id);
            return Ok(item);
        }
        /// <summary>
        /// جستجو
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        [ProducesResponseType(typeof(PostLangSearchDto), 200)]
        public IActionResult Get([FromQuery] PostLangInputDto dto)
        {
            var item = postLangService.SearchDto(dto);
            return Ok(item);

        }

        /// <summary>
        /// اضافه و ویرایش آیتم
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<PostLangDto>), 200)]
        public async Task<IActionResult> Post(PostLangDto dto)
        {
            var item = await postLangService.InsertAndUpdateAsyncDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// حذف آیتم(ItemId=PostId)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(OtherLangDeleteDto), 200)]
        public IActionResult Delete(OtherLangDeleteDto dto)
        {
            var item = postLangService.DeleteDto(dto);
            return Ok(item);
        }

    }
}
