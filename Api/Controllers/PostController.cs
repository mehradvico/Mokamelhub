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
    public class PostController : ControllerBase
    {
        private IPostService postService;
        /// <summary>
        /// مرتبط با پست ها
        /// </summary>
        public PostController(IPostService postService)
        {
            this.postService = postService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id">شناسه دسته بندی</param>
        /// <returns></returns>
        /// 
        [HttpGet("{id}")]
        [CustomOutputCache(CacheTypeEnum.PostOne)]
        [ProducesResponseType(typeof(BaseResultDto<PostVDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var post = await postService.FindAsyncVDto(id, visit: true);
            return Ok(post);
        }


        /// <summary>
        /// جستجو
        /// </summary>
        [HttpGet]
        [CustomOutputCache(CacheTypeEnum.PostSearch)]
        [ProducesResponseType(typeof(PostSearchDto), 200)]
        public IActionResult Get([FromQuery] PostInputDto dto)
        {
            dto.Available = true;
            var post = postService.Search(dto);
            return Ok(post);
        }

    }
}
