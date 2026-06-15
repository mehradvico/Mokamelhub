using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.GalleryLangSrv.Dto;
using Application.Services.Language.GalleryLangSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت زبان گالری ها
    /// </summary>
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class GalleryLanguageController : ControllerBase
    {
        private readonly IGalleryLangService galleryLangService;
        /// <summary>
        /// مدیریت زبان گالری ها
        /// </summary>
        ///   
        public GalleryLanguageController(IGalleryLangService galleryLangService)
        {
            this.galleryLangService = galleryLangService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<GalleryLangDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var item = await galleryLangService.FindAsyncDto(id);
            return Ok(item);
        }
        /// <summary>
        /// جستجو
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(GalleryLangSearchDto), 200)]
        public IActionResult Get([FromQuery] GalleryLangInputDto dto)
        {
            var item = galleryLangService.SearchDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// اضافه و ویرایش آیتم
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<GalleryLangDto>), 200)]
        public async Task<IActionResult> Post(GalleryLangDto dto)
        {
            var item = await galleryLangService.InsertAndUpdateAsyncDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// حذف آیتم(ItemId=GalleryId)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(OtherLangDeleteDto), 200)]
        public IActionResult Delete(OtherLangDeleteDto dto)
        {
            var item = galleryLangService.DeleteDto(dto);
            return Ok(item);
        }

    }
}
