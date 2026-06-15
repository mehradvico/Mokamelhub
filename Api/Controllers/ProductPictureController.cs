using Application.Common.Enumerable;
using Application.Services.ProductSrvs.ProductPictureSrv.Dto;
using Application.Services.ProductSrvs.ProductPictureSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با تصاویر محصول
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductPictureController : ControllerBase
    {
        private IProductPictureService ProductPictureService;
        /// <summary>
        /// مرتبط با تصاویر محصول
        /// </summary>
        public ProductPictureController(IProductPictureService ProductPictureService)
        {
            this.ProductPictureService = ProductPictureService;
        }


        /// <summary>
        /// جستجو
        /// </summary>
        [HttpGet]
        [CustomOutputCache(CacheTypeEnum.ProductPicture)]
        [ProducesResponseType(typeof(ProductPictureSearchDto), 200)]
        public IActionResult Get([FromQuery] ProductPictureInputDto dto)
        {
            var ProductPicture = ProductPictureService.SearchDto(dto);
            return Ok(ProductPicture);
        }
    }
}
