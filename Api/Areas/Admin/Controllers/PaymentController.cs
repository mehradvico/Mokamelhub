using Application.Common.Dto.Result;
using Application.Services.Order.PaymentSrv.Iface;
using Application.Services.Order.ProductOrderSrv.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// برای تراکنش‌هایی که در درگاه با موفقیت پرداخت شده‌اند ولی به دلیل عدم بازگشت
        /// مرورگر کاربر (بسته‌شدن تب، قطعی اینترنت و ...) وضعیت سفارش «در انتظار» باقی مانده،
        /// این متد مستقیماً از درگاه استعلام می‌گیرد و در صورت موفقیت سفارش را تسویه می‌کند.
        /// </summary>
        [HttpPost("{paymentId}/recheck")]
        [ProducesResponseType(typeof(BaseResultDto<PaymentDto>), 200)]
        public async Task<IActionResult> Recheck(long paymentId)
        {
            return Ok(await _paymentService.RecheckPayment(paymentId));
        }
    }
}
