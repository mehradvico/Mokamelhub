using Application.Common.Enumerable;
using Application.Services.ProductSrvs.ProductFileSrv.Dto;
using Application.Services.ProductSrvs.ProductFileSrv.Iface;
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
    public class ProductFileController : ControllerBase
    {
        private IProductFileService ProductFileService;
        /// <summary>
        /// مرتبط با تصاویر محصول
        /// </summary>
        public ProductFileController(IProductFileService ProductFileService)
        {
            this.ProductFileService = ProductFileService;
        }


        /// <summary>
        /// جستجو
        /// </summary>
        [HttpGet]
        [CustomOutputCache(CacheTypeEnum.ProductFile)]
        [ProducesResponseType(typeof(ProductFileSearchDto), 200)]
        public IActionResult Get([FromQuery] ProductFileInputDto dto)
        {
            var ProductFile = ProductFileService.SearchDto(dto);
            return Ok(ProductFile);
        }
    }
}
