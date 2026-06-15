using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.ProductSrvs.ProductLikeSrv.Dto;
using Application.Services.ProductSrvs.ProductLikeSrv.Iface;
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
    public class ProductLikeController : ControllerBase
    {
        private IProductLikeService ProductLikeService;
        private ICurrentUserHelper _currentUserHelper;
        /// <summary>
        /// مرتبط با نظر محصول
        /// </summary>
        public ProductLikeController(IProductLikeService ProductLikeService, ICurrentUserHelper currentUserHelper)
        {
            this.ProductLikeService = ProductLikeService;
            this._currentUserHelper = currentUserHelper;
        }
        /// <summary>
        /// جستجو
        /// </summary>

        /// 
        [HttpGet]
        [ProducesResponseType(typeof(ProductLikeSearchDto), 200)]
        public IActionResult Get([FromQuery] ProductLikeInputDto dto)
        {
            dto.Available = true;
            dto.UserId = _currentUserHelper.CurrentUser.UserId;
            var post = ProductLikeService.SearchDto(dto);
            return Ok(post);
        }
        /// <summary>
        /// آیتم جدید
        /// </summary>  
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<ProductLikeDto>), 200)]
        public async Task<IActionResult> Post(ProductLikeDto productLike)
        {
            productLike.UserId = _currentUserHelper.CurrentUser.UserId;
            var dto = await ProductLikeService.InsertAsyncDto(productLike);
            return Ok(dto);
        }
        /// <summary>
        /// حذف آیتم 
        /// </summary>  
        [HttpDelete]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public IActionResult Delete(ProductLikeDto productLike)
        {
            productLike.UserId = _currentUserHelper.CurrentUser.UserId;
            var dto = ProductLikeService.DeleteDto(productLike);
            return Ok(dto);
        }

    }
}
