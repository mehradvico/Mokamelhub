using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.ProductSrv.Dto;
using Application.Services.ProductSrvs.ProductSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با سایت مپ محصولات
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductSiteMapController : ControllerBase
    {
        private IProductService productService;
        /// <summary>
        /// مرتبط با سایت مپ محصولات
        /// </summary>
        public ProductSiteMapController(IProductService productService)
        {
            this.productService = productService;
        }
        /// <summary>
        /// سایت مپ محصولات
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet()]
        [CustomOutputCache(CacheTypeEnum.ProductSiteMap)]
        [ProducesResponseType(typeof(BaseResultDto<ProductSiteMapDto>), 200)]
        public IActionResult Get()
        {
            var list = productService.GetSiteMap();
            return Ok(list);
        }

    }
}
