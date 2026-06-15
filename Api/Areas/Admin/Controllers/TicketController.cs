using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Services.Accounting.TicketSrv.Dto;
using Application.Services.Accounting.TicketSrv.Iface;
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
    public class TicketController : ControllerBase
    {
        private ITicketService TicketService;
        /// <summary>
        /// مدیریت تیکت ها
        /// </summary>
        ///
        public TicketController(ITicketService TicketService)
        {
            this.TicketService = TicketService;
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
            var role = await TicketService.FindAsyncVDto(id);
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

            var dto = await TicketService.InsertAdminAsyncDto(TicketDto);
            return Ok(dto);
        }
        /// <summary>
        /// ویرایش آیتم
        /// </summary>

        [HttpPut]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public IActionResult Put(TicketDto TicketDto)
        {
            var dto = TicketService.UpdateDto(TicketDto);
            return Ok(dto);
        }

        /// <summary>
        /// تغییر کاربر
        /// </summary>

        [HttpPut("ChangeAdmin")]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public IActionResult ChangeAdmin(TicketDto TicketDto)
        {
            var dto = TicketService.ChangeAdmin(TicketDto);
            return Ok(dto);
        }
        /// <summary>
        /// حذف آیتم
        /// </summary>
        ///
        [HttpDelete]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public IActionResult Delete(long id)
        {
            var dto = TicketService.DeleteDto(id);
            return Ok(dto);
        }
    }
}
