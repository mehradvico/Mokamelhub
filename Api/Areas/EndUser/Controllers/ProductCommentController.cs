using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Dto;
using Application.Services.ProductSrvs.ProductCommentSrv.Dto;
using Application.Services.ProductSrvs.ProductCommentSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.EndUser.Controllers
{
    /// <summary>
    /// مرتبط با نظر محصول
    /// </summary>
    [Area("EndUser")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductCommentController : ControllerBase
    {
        private IProductCommentService ProductCommentService;
        private readonly CurrentUserDto _currentUser;
        /// <summary>
        /// مرتبط با نظر محصول
        /// </summary>
        public ProductCommentController(IProductCommentService ProductCommentService, ICurrentUserHelper currentUserHelper)
        {
            this.ProductCommentService = ProductCommentService;
            _currentUser = currentUserHelper.CurrentUser;

        }
        /// <summary>
        /// جستجو
        /// </summary>

        /// 
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProductCommentSearchDto), 200)]
        public IActionResult Get([FromQuery] ProductCommentInputDto dto)
        {
            dto.Available = true;
            dto.AllStatus = false;
            if (_currentUser != null)
            {
                dto.UserId = _currentUser.UserId;
            }
            var post = ProductCommentService.Search(dto);
            return Ok(post);
        }
        /// <summary>
        /// آیتم جدید
        /// </summary>  
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<ProductCommentDto>), 200)]
        public async Task<IActionResult> Post(ProductCommentDto ProductComment)
        {
            ProductComment.UserId = _currentUser.UserId;
            var dto = await ProductCommentService.InsertAsyncDto(ProductComment);
            return Ok(dto);
        }

    }
}
