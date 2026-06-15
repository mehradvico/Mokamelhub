using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.ProductSrv.Dto;
using Application.Services.ProductSrvs.ProductSrv.Dto;
using Application.Services.ProductSrvs.ProductSrv.Iface;
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
    public class ProductController : ControllerBase
    {
        private IProductService ProductService;
        /// <summary>
        /// مرتبط با محصولات 
        /// </summary>
        public ProductController(IProductService ProductService)
        {
            this.ProductService = ProductService;
        }

        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id">شناسه</param>
        /// <returns></returns>
        /// 
        [HttpGet("{id:long}")]
        [CustomOutputCache(CacheTypeEnum.ProductOne)]
        [ProducesResponseType(typeof(BaseResultDto<ProductVDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var Product = await ProductService.FindAsyncVDto(id, visit: true);
            return Ok(Product);
        }

        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="label">لیبل</param>
        /// <returns></returns>
        /// 
        [HttpGet("{label}")]
        [CustomOutputCache(CacheTypeEnum.ProductOne)]
        [ProducesResponseType(typeof(BaseResultDto<ProductVDto>), 200)]
        public async Task<IActionResult> Get(string label)
        {
            var product = await ProductService.FindAsyncVDto(label, visit: true);
            return Ok(product);
        }


        /// <summary>
        /// جستجو
        /// </summary>
        [HttpGet]
        [CustomOutputCache(CacheTypeEnum.ProductSearch)]
        [ProducesResponseType(typeof(ProductSearchDto), 200)]
        public IActionResult Get([FromQuery] ProductInputDto dto)
        {
            dto.Available = true;
            var Product = ProductService.Search(dto);
            return Ok(Product);
        }

    }
}
