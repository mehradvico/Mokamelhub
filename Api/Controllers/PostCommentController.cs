using Application.Common.Dto.Result;
using Application.Services.Content.PostCommentSrv.Dto;
using Application.Services.Content.PostCommentSrv.Iface;
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
    public class PostCommentController : ControllerBase
    {
        private IPostCommentService postCommentService;
        /// <summary>
        /// مرتبط با پست ها
        /// </summary>
        public PostCommentController(IPostCommentService postCommentService)
        {
            this.postCommentService = postCommentService;
        }
        /// <summary>
        /// جستجو
        /// </summary>

        /// 
        [HttpGet]
        [ProducesResponseType(typeof(PostCommentSearchDto), 200)]
        public IActionResult Get([FromQuery] PostCommentInputDto dto)
        {
            dto.Available = true;
            var post = postCommentService.Search(dto);
            return Ok(post);
        }
        /// <summary>
        /// آیتم جدید
        /// </summary>  
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResultDto<PostCommentDto>), 200)]
        public async Task<IActionResult> Post(PostCommentDto postComment)
        {
            var dto = await postCommentService.InsertAsyncDto(postComment);
            return Ok(dto);
        }

    }
}
