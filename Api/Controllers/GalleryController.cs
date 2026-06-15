using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.Content.GallerySrv.Dto;
using Application.Services.Content.GallerySrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با دسته بندی ها
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class GalleryController : ControllerBase
    {
        private IGalleryService GalleryService;
        /// <summary>
        /// مرتبط با پست ها
        /// </summary>
        public GalleryController(IGalleryService GalleryService)
        {
            this.GalleryService = GalleryService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="label">برچسب</param>
        /// <returns></returns>
        /// 
        [HttpGet("label/{label}")]
        [CustomOutputCache(CacheTypeEnum.GalleryOne)]
        [ProducesResponseType(typeof(BaseResultDto<GalleryVDto>), 200)]
        public async Task<IActionResult> Get(string label)
        {
            var Gallery = await GalleryService.FindVDtoAsync(label);
            return Ok(Gallery);
        }


        /// <summary>
        /// جستجو
        /// </summary>
        [HttpGet]
        [CustomOutputCache(CacheTypeEnum.GallerySearch)]
        [ProducesResponseType(typeof(GallerySearchDto), 200)]
        public IActionResult Get([FromQuery] GalleryInputDto dto)
        {
            dto.Available = true;
            var post = GalleryService.Search(dto);
            return Ok(post);
        }
    }
}
