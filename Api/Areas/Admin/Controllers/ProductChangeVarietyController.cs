using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.ProductSrv.Dto;
using Application.Services.ProductSrvs.ProductSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// تغییر تنوع محصول
    /// </summary>
    ///
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductChangeVarietyController : ControllerBase
    {
        private readonly IProductService _productService;
        /// <summary>
        /// تغییر تنوع محصول
        /// </summary>
        ///
        public ProductChangeVarietyController(IProductService productService)
        {
            this._productService = productService;
        }

        /// <summary>
        /// تغییر تنوع محصول
        /// </summary>

        [HttpPut]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public async Task<IActionResult> Put(ProductDto productDto)
        {
            var dto = await _productService.ChangeProductVarietiesAsync(productDto);
            return Ok(dto);
        }

    }
}
