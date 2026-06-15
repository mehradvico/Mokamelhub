using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.ProductVariety.Dto;
using Application.Services.ProductSrvs.ProductVariety.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت تنوع های محصولات
    /// </summary>
    [Area("Admin")]
    [Route("api/[area]/[Controller]")]
    [ApiController]
    [Authorize]
    public class ProductVariety : ControllerBase
    {
        private IProductVarietyService productVarietyService;
        /// <summary>
        /// مدیریت تنوع های محصول
        /// </summary>
        public ProductVariety(IProductVarietyService productVarietyService)
        {
            this.productVarietyService = productVarietyService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>  
        [HttpGet]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public async Task<IActionResult> Get(long productId)
        {
            var dto = await productVarietyService.FindAsyncDto(productId);
            return Ok(dto);
        }
        /// <summary>
        /// اضافه و ویرایش تنوع محصول
        /// </summary>  
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public async Task<IActionResult> Post(ProductVarietyDto dto)
        {
            var result = await productVarietyService.InsertAndUpdateAsyncDto(dto);
            return Ok(result);
        }
    }
}
