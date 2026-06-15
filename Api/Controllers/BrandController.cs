using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.ProductSrvs.BrandSrv.Dto;
using Application.Services.ProductSrvs.BrandSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با برند ها
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BrandController : ControllerBase
    {
        private IBrandService brandService;
        /// <summary>
        /// مرتبط با برند ها
        /// </summary>
        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }


        /// <summary>
        /// جستجو
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(BrandSearchDto), 200)]
        [CustomOutputCache(CacheTypeEnum.BrandSearch)]

        public IActionResult Get([FromQuery] BrandInputDto dto)
        {
            dto.Available = true;
            var post = brandService.Search(dto);
            return Ok(post);
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        [HttpGet("Id")]
        [ProducesResponseType(typeof(BaseResultDto<BrandVDto>), 200)]
        [CustomOutputCache(CacheTypeEnum.BrandOne)]
        public async Task<IActionResult> Get(long id)
        {
            var post = await brandService.FindAsyncVDto(id);
            return Ok(post);
        }
        /// <summary>
        /// برندهای مربوط به دسته بندی و والدهای دسته بندی
        /// </summary>
        [CustomOutputCache(CacheTypeEnum.BrandCategory)]
        [HttpGet("category/{categoryLabel}")]
        [ProducesResponseType(typeof(BaseResultDto<BrandVDto>), 200)]
        public IActionResult Get([FromQuery] string categoryLabel)
        {
            var post = brandService.GetForCategory(categoryLabel);
            return Ok(post);
        }
    }
}
