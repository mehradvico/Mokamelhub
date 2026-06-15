using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.ProductItemSrv.Dto;
using Application.Services.ProductSrvs.ProductItemSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با تنوع محصولات 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductItemController : ControllerBase
    {
        private IProductItemService ProductItemService;
        /// <summary>
        /// مرتبط با تنوع محصولات 
        /// </summary>
        public ProductItemController(IProductItemService ProductItemService)
        {
            this.ProductItemService = ProductItemService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id">شناسه</param>
        /// <returns></returns>
        /// 
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<List<ProductItemForProductVDto>>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var ProductItem = await ProductItemService.GetForProductVDto(id);
            return Ok(ProductItem);
        }

    }
}
