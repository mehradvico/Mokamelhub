using Microsoft.AspNetCore.Mvc;
using Utility.BackgroundTask.Iface;
using Utility.Reflection.Iface;

namespace Api.Controllers
{
    /// <summary>
    /// پس زمینه
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BackgroundTaskController : ControllerBase
    {
        private readonly IBackgroundTask backgroundTask;
        private readonly IControllerActionDiscoveryService _controllerActionDiscoveryService;
        /// <summary>
        /// مرتبط با پس زمینه
        /// </summary>
        public BackgroundTaskController(IBackgroundTask backgroundTask, IControllerActionDiscoveryService controllerActionDiscoveryService)
        {
            this.backgroundTask = backgroundTask;
            this._controllerActionDiscoveryService = controllerActionDiscoveryService;
        }
        /// <summary>
        /// اجرا پس زمینه
        /// </summary>
        ///        
        [HttpGet]
        public IActionResult Get()
        {

            backgroundTask.StartSyncSmsAsync();
            backgroundTask.StartSyncCloseTicketAsync();
            backgroundTask.StartSyncExpiredDiscountAsync();
            return Ok();
        }
    }
}
