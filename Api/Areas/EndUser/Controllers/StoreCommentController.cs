using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.ProductSrvs.StoreCommentSrv.Dto;
using Application.Services.ProductSrvs.StoreCommentSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.EndUser.Controllers
{
    /// <summary>
    /// مرتبط با فروشگاه ها
    /// </summary>
    [Area("EndUser")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreCommentController : ControllerBase
    {
        private IStoreCommentService _storeCommentService;
        private ICurrentUserHelper _currentUserHelper;
        /// <summary>
        /// مرتبط با فروشگاه ها
        /// </summary>
        public StoreCommentController(IStoreCommentService storeCommentService, ICurrentUserHelper currentUserHelper)
        {
            this._storeCommentService = storeCommentService;
            this._currentUserHelper = currentUserHelper;
        }
        /// <summary>
        /// جستجو
        /// </summary>

        /// 
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(StoreCommentSearchDto), 200)]
        public IActionResult Get([FromQuery] StoreCommentInputDto dto)
        {
            dto.Available = true;
            dto.AllStatus = false;
            var post = _storeCommentService.Search(dto);
            return Ok(post);
        }
        /// <summary>
        /// آیتم جدید
        /// </summary>  
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<StoreCommentDto>), 200)]
        public async Task<IActionResult> Post(StoreCommentDto storeComment)
        {
            storeComment.UserId = _currentUserHelper.CurrentUser.UserId;
            var dto = await _storeCommentService.InsertAsyncDto(storeComment);
            return Ok(dto);
        }

    }
}
