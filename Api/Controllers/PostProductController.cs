using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.Content.PostProductSrv.Dto;
using Application.Services.Content.PostProductSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با محصولات مقاله
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PostProductController : ControllerBase
    {
        private IPostProductService postProductService;
        /// <summary>
        /// مرتبط با محصولات مقاله
        /// </summary>
        public PostProductController(IPostProductService postProductService)
        {
            this.postProductService = postProductService;
        }
        /// <summary>
        ///  اطلاعات آیتم 
        /// </summary>
        /// <param name="id">شناسه</param>
        /// <returns>
        /// </returns>
        [HttpGet("{id}")]
        [CustomOutputCache(CacheTypeEnum.ProductPost)]
        [ProducesResponseType(typeof(BaseResultDto<PostProductDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var item = await postProductService.GetPostProductsAsync(id);
            return Ok(item);
        }

    }
}
