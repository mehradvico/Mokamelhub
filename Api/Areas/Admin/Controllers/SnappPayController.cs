using Application.Common.Dto.Result;
using Application.Services.Order.SnappPaySrv.Dto;
using Application.Services.Order.SnappPaySrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class SnappPayController : ControllerBase
    {
        private readonly ISnappPayService _snappPayService;

        public SnappPayController(ISnappPayService snappPayService)
        {
            _snappPayService = snappPayService;
        }

        [HttpGet("order/{orderId}/status")]
        [ProducesResponseType(typeof(BaseResultDto<SnappPayOrderOperationResultDto>), 200)]
        public async Task<IActionResult> Status(string orderId, [FromQuery] bool refresh = true)
        {
            return Ok(await _snappPayService.GetOrderStatusAsync(orderId, refresh));
        }

        [HttpPost("order/{orderId}/update")]
        [ProducesResponseType(typeof(BaseResultDto<SnappPayOrderOperationResultDto>), 200)]
        public async Task<IActionResult> Update(string orderId, SnappPayUpdateOrderDto dto)
        {
            return Ok(await _snappPayService.UpdateOrderAsync(orderId, dto));
        }

        [HttpPost("order/{orderId}/cancel")]
        [ProducesResponseType(typeof(BaseResultDto<SnappPayOrderOperationResultDto>), 200)]
        public async Task<IActionResult> Cancel(string orderId, SnappPayCancelOrderDto dto)
        {
            return Ok(await _snappPayService.CancelOrderAsync(orderId, dto));
        }
    }
}
