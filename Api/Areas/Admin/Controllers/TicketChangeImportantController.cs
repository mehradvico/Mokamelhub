using Application.Common.Dto.Result;
using Application.Services.Accounting.TicketSrv.Dto;
using Application.Services.Accounting.TicketSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// تغییر وضعیت تیکت ها
    /// </summary>
    ///
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketChangeImportantController : ControllerBase
    {
        private ITicketService TicketService;
        /// <summary>
        /// تغییر اهمیت تیکت ها
        /// </summary>
        ///
        public TicketChangeImportantController(ITicketService TicketService)
        {
            this.TicketService = TicketService;
        }

        /// <summary>
        /// تغییر اهمیت تیکت ها
        /// </summary>

        [HttpPut("")]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public IActionResult Put(TicketDto TicketDto)
        {
            var dto = TicketService.ChangeImportance(TicketDto);
            return Ok(dto);
        }

    }
}
