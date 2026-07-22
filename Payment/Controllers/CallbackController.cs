using Application.Common.Helpers;
using Application.Common.Helpers.Iface;
using Application.Services.Order.PaymentSrv.Iface;
using Microsoft.AspNetCore.Mvc;

namespace Payment.Controllers
{
    public class CallbackController : Controller
    {
        private readonly ILogger<CallbackController> _logger;
        private readonly IPaymentService _paymentService;
        private readonly IAdminSettingHelper _adminSettingHelper;
        public CallbackController(ILogger<CallbackController> logger, IPaymentService paymentService, IAdminSettingHelper adminSettingHelper)
        {
            _logger = logger;
            _paymentService = paymentService;
            _adminSettingHelper = adminSettingHelper;
        }
        [HttpGet("/callback/{id:long}")]
        [HttpPost("/callback/{id:long}")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> Index(long id)
        {
            var payment = await _paymentService.CallbackPayment(id);

            TempData["ReturnToSiteUrl"] = string.IsNullOrWhiteSpace(AppSettingsHelper.BaseUrl)
                ? "https://mokamelhub.com"
                : AppSettingsHelper.BaseUrl.TrimEnd('/');

            return View(payment);
        }
    }
}
