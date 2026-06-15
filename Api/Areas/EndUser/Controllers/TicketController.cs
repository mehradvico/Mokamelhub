using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Accounting.TicketSrv.Dto;
using Application.Services.Accounting.TicketSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.EndUser.Controllers
{
    /// <summary>
    /// مدیریت تیکت ها
    /// </summary>
    ///
    [Area("EndUser")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly long _currentUserId;

        private readonly ITicketService TicketService;
        /// <summary>
        /// مدیریت تیکت ها
        /// </summary>
        ///
        public TicketController(ITicketService TicketService, ICurrentUserHelper currentUserHelper)
        {
            this.TicketService = TicketService;
            _currentUserId = currentUserHelper.CurrentUser.UserId;
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
            var role = await TicketService.UserFindAsyncVDto(id);
            return Ok(role);
        }
        /// <summary>
        ///  جستجو
        /// </summary>
        /// <returns></returns> 

        [HttpGet]
        [ProducesResponseType(typeof(BaseInputDto), 200)]
        public IActionResult Get([FromQuery] TicketInputDto dto)
        {
            dto.UserId = _currentUserId;
            var searchDto = TicketService.Search(dto);
            return Ok(searchDto);
        }
        /// <summary>
        /// آیتم جدید
        /// </summary>  
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<TicketDto>), 200)]
        public async Task<IActionResult> Post(TicketDto TicketDto)
        {

            var dto = await TicketService.InsertUserAsyncDto(TicketDto);
            return Ok(dto);
        }

    }
}
