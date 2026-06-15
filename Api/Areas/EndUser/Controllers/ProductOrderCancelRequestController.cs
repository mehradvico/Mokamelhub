using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Order.ProductOrderSrv.Dto;
using Application.Services.Order.ProductOrderSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.EndUser.Controllers
{
    /// <summary>
    /// مدیریت سفارش ها
    /// </summary>
    ///
    [Area("EndUser")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductOrderCancelRequestController : ControllerBase
    {
        private IProductOrderService productOrderService;
        private ICurrentUserHelper _currentUserHelper;

        /// <summary>
        /// مدیریت سفارش ها
        /// </summary>
        ///
        public ProductOrderCancelRequestController(IProductOrderService productOrder, ICurrentUserHelper currentUserHelper)
        {
            productOrderService = productOrder;
            _currentUserHelper = currentUserHelper;
        }
        /// <summary>
        ///  اطلاعات آیتم 
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public async Task<IActionResult> Post(ProductOrderDto productOrder)
        {
            productOrder.CancelRequest = DateTime.Now;
            productOrder.UserId = _currentUserHelper.CurrentUser.UserId;
            var dto = await productOrderService.SetCancelRequestAsync(productOrder);
            return Ok(dto);
        }
    }
}
