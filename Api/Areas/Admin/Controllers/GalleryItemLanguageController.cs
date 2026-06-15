using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.GalleryItemLangSrv.Dto;
using Application.Services.Language.GalleryItemLangSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت زبان گالری آیتم ها
    /// </summary>
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class GalleryItemLanguageController : ControllerBase
    {
        private readonly IGalleryItemLangService galleryItemLangService;
        /// <summary>
        /// مدیریت زبان گالری آیتم ها
        /// </summary>
        ///  
        public GalleryItemLanguageController(IGalleryItemLangService galleryItemLangService)
        {
            this.galleryItemLangService = galleryItemLangService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<GalleryItemLangDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var item = await galleryItemLangService.FindAsyncDto(id);
            return Ok(item);
        }
        /// <summary>
        /// جستجو
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(GalleryItemLangSearchDto), 200)]
        public IActionResult Get([FromQuery] GalleryItemLangInputDto dto)
        {
            var item = galleryItemLangService.SearchDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// اضافه و ویرایش آیتم
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<GalleryItemLangDto>), 200)]
        public async Task<IActionResult> Post(GalleryItemLangDto dto)
        {
            var item = await galleryItemLangService.InsertAndUpdateAsyncDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// حذف آیتم(ItemId=GalleryItemId)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(OtherLangDeleteDto), 200)]
        public IActionResult Delete(OtherLangDeleteDto dto)
        {
            var item = galleryItemLangService.DeleteDto(dto);
            return Ok(item);
        }
    }
}
