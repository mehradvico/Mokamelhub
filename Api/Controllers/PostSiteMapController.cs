using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.Content.PostSrv.Dto;
using Application.Services.Content.PostSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با پست ها
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PostSiteMapController : ControllerBase
    {
        private IPostService postService;
        /// <summary>
        /// مرتبط با سایت مپ پست ها
        /// </summary>
        public PostSiteMapController(IPostService postService)
        {
            this.postService = postService;
        }
        /// <summary>
        /// سایت مپ پست ها
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet()]
        [CustomOutputCache(CacheTypeEnum.PostSiteMap)]
        [ProducesResponseType(typeof(BaseResultDto<PostSiteMapDto>), 200)]
        public IActionResult Get()
        {
            var list = postService.GetSiteMap();
            return Ok(list);
        }

    }
}
