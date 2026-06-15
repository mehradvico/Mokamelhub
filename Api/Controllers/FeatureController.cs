using Application.Common.Enumerable;
using Application.Services.ProductSrvs.FeatureSrv.Dto;
using Application.Services.ProductSrvs.FeatureSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با بنر ها
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FeatureController : ControllerBase
    {
        private IFeatureService FeatureService;
        /// <summary>
        /// مرتبط با بنر ها
        /// </summary>
        public FeatureController(IFeatureService FeatureService)
        {
            this.FeatureService = FeatureService;
        }


        /// <summary>
        /// جستجو
        /// </summary>
        [HttpGet]
        [CustomOutputCache(CacheTypeEnum.Feature)]
        [ProducesResponseType(typeof(FeatureSearchDto), 200)]
        public IActionResult Get([FromQuery] FeatureInputDto dto)
        {
            dto.Available = true;
            var post = FeatureService.GetForCategory(dto);
            return Ok(post);
        }
    }
}
