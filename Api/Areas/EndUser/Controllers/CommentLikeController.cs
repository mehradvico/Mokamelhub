using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.CommonSrv.CommentLikeSrv.Dto;
using Application.Services.CommonSrv.CommentLikeSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.EndUser.Controllers
{
    /// <summary>
    /// مرتبط با لایک کامنت ها
    /// </summary>
    /// 
    [Area("EndUser")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentLikeController : ControllerBase
    {
        private readonly ICommentLikeService _commentLikeService;
        private readonly ICurrentUserHelper _currentUser;
        /// <summary>
        /// مرتبط با لایک کامنت ها
        /// </summary>
        ///
        public CommentLikeController(ICommentLikeService commentLikeService, ICurrentUserHelper currentUser)
        {
            this._commentLikeService = commentLikeService;
            this._currentUser = currentUser;
        }

        /// <summary>
        /// آیتم جدید
        /// </summary>  
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<CommentLikeDto>), 200)]
        public async Task<IActionResult> Post(CommentLikeInsertDeleteDto commentlike)
        {
            var dto = await _commentLikeService.InsertOrDeleteAsync(commentlike);
            return Ok(dto);
        }
    }
}
