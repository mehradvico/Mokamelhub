using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.ProductSrvs.ProductRelateSrv.Dto;
using Application.Services.ProductSrvs.ProductRelateSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با محصولات 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductRelateController : ControllerBase
    {
        private IProductRelateService ProductRelateService;
        /// <summary>
        /// مرتبط با محصولات 
        /// </summary>
        public ProductRelateController(IProductRelateService ProductRelateService)
        {
            this.ProductRelateService = ProductRelateService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <returns></returns>
        /// 
        [ProducesResponseType(typeof(BaseResultDto<ProductRelateVDto>), 200)]
        [CustomOutputCache(CacheTypeEnum.ProductRelated)]
        [HttpGet]
        public IActionResult Get([FromQuery] ProductRelateDto dto)
        {
            var ProductRelate = ProductRelateService.GetForProduct(dto);
            return Ok(ProductRelate);
        }
    }
}
