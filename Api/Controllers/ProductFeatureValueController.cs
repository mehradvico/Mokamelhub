using Application.Services.ProductSrvs.FeatureItemSrv.Dto;
using Application.Services.ProductSrvs.ProductFeatureValueSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با ویژگی ها
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductFeatureValueController : ControllerBase
    {
        private IProductFeatureValueService ProductFeatureValueService;
        /// <summary>
        /// مرتبط با ویژگی ها
        /// </summary>
        public ProductFeatureValueController(IProductFeatureValueService ProductFeatureValueService)
        {
            this.ProductFeatureValueService = ProductFeatureValueService;
        }


        /// <summary>
        /// جستجو
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ProductFeatureValueSearchDto), 200)]
        //[OutputCache(Duration = 5000)]

        public IActionResult Get([FromQuery] ProductFeatureValueInputDto dto)
        {
            dto.Available = true;
            var post = ProductFeatureValueService.Search(dto);
            return Ok(post);
        }
    }
}
