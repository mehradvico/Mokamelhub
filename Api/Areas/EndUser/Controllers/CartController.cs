using Application.Common.Dto.Result;
using Application.Services.Order.CartSrv.Dto;
using Application.Services.Order.CartSrv.Iface;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.EndUser.Controllers
{
    /// <summary>
    /// سبد خرید
    /// </summary>
    ///
    [Area("EndUser")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    //[Authorize]
    public class CartController : ControllerBase
    {
        private ICartService CartService;
        /// <summary>
        /// سبد خرید
        /// </summary>
        ///
        public CartController(ICartService CartService)
        {
            this.CartService = CartService;
        }
        /// <summary>
        /// ویرایش Code 1: کاربر باید لاگین کند
        /// </summary>  
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<CartVDto>), 200)]
        public async Task<IActionResult> Post(CartUpdateDto cartUpdate)
        {
            var dto = await CartService.CartUpdateAsync(cartUpdate);
            return Ok(dto);
        }

    }


}
