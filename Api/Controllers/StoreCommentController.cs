using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.StoreCommentSrv.Dto;
using Application.Services.ProductSrvs.StoreCommentSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با فروشگاه ها
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class StoreCommentController : ControllerBase
    {
        private IStoreCommentService _storeCommentService;
        /// <summary>
        /// مرتبط با پست ها
        /// </summary>
        public StoreCommentController(IStoreCommentService storeCommentService)
        {
            this._storeCommentService = storeCommentService;
        }
        /// <summary>
        /// جستجو
        /// </summary>
        /// 
        [HttpGet]
        [ProducesResponseType(typeof(StoreCommentSearchDto), 200)]
        public IActionResult Get([FromQuery] StoreCommentInputDto dto)
        {
            dto.Available = true;
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
            var dto = await _storeCommentService.InsertAsyncDto(storeComment);
            return Ok(dto);
        }
    }
}
