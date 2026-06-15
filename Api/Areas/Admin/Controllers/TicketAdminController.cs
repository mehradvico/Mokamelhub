using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Accounting.TicketSrv.Dto;
using Application.Services.Accounting.TicketSrv.Iface;
using Application.Services.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت تیکت ها
    /// </summary>
    ///
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketAdminController : ControllerBase
    {
        private readonly ITicketService TicketService;
        private readonly CurrentUserDto _currentUser;
        /// <summary>
        /// مدیریت تیکت ها
        /// </summary>
        ///
        public TicketAdminController(ITicketService TicketService, ICurrentUserHelper currentUserHelper)
        {
            this.TicketService = TicketService;
            this._currentUser = currentUserHelper.CurrentUser;
        }
        /// <summary>
        ///  اطلاعات آیتم 
        /// </summary>
        /// <param name="id">شناسه</param>
        /// <returns>
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<TicketVDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var ticket = await TicketService.FindAsyncVDto(id, _currentUser.UserId);
            return Ok(ticket);
        }
        /// <summary>
        ///  جستجو
        /// </summary>
        /// <returns></returns> 

        [HttpGet]
        [ProducesResponseType(typeof(BaseInputDto), 200)]
        public IActionResult Get([FromQuery] TicketInputDto dto)
        {
            dto.AllAdminId = false;
            dto.AdminId = _currentUser.UserId;
            var searchDto = TicketService.Search(dto);
            return Ok(searchDto);
        }

    }
}
