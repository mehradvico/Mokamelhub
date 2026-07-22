using Application.Common.Dto.Result;
using Application.Services.Order.SnappPaySrv.Dto;
using Application.Services.Order.SnappPaySrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Areas.EndUser.Controllers
{
    [Area("EndUser")]
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

        [HttpGet("eligible")]
        [ProducesResponseType(typeof(BaseResultDto<SnappPayEligibilityResponse>), 200)]
        public async Task<IActionResult> Eligible([FromQuery] double amountToman, [FromQuery] List<string> paymentMethodTypes)
        {
            var result = await _snappPayService.GetEligibilityAsync(amountToman, paymentMethodTypes);
            return Ok(result);
        }
    }
}
