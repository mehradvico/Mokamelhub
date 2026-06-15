using Application.Common.Dto.Result;
using Application.Services.Content.GalleryItemSrv.Dto;
using Application.Services.Content.GalleryItemSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مدیریت گالری آیتم ها
    /// </summary>
    ///
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class GalleryItemController : ControllerBase
    {
        private IGalleryItemService galleryItemService;
        /// <summary>
        /// مدیریت گالری آیتم ها
        /// </summary>
        ///
        public GalleryItemController(IGalleryItemService galleryItemService)
        {
            this.galleryItemService = galleryItemService;
        }

        /// <summary>
        ///  جستجو
        /// </summary>
        /// <returns></returns> 

        [HttpGet]
        [ProducesResponseType(typeof(BaseResultDto<GalleryItemDto>), 200)]
        public IActionResult Get([FromQuery] GalleryItemInputDto dto)
        {
            dto.Available = true;
            var searchDto = galleryItemService.Search(dto);
            return Ok(searchDto);
        }

    }
}
