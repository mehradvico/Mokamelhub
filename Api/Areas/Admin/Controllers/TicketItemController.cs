using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Services.Accounting.TicketItemSrv.Dto;
using Application.Services.Accounting.TicketItemSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت آیتم تیکت ها
    /// </summary>
    ///
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketItemController : ControllerBase
    {
        private ITicketItemService TicketItemService;
        /// <summary>
        /// مدیریت آیتم تیکت ها
        /// </summary>
        ///
        public TicketItemController(ITicketItemService TicketItemService)
        {
            this.TicketItemService = TicketItemService;
        }
        /// <summary>
        ///  اطلاعات آیتم 
        /// </summary>
        /// <param name="id">شناسه</param>
        /// <returns>
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<TicketItemDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var role = await TicketItemService.FindAsyncDto(id);
            return Ok(role);
        }
        /// <summary>
        ///  جستجو
        /// </summary>
        /// <returns></returns> 

        [HttpGet]
        [ProducesResponseType(typeof(BaseInputDto), 200)]
        public IActionResult Get([FromQuery] TicketItemInputDto dto)
        {
            var searchDto = TicketItemService.Search(dto);
            return Ok(searchDto);
        }
        /// <summary>
        /// آیتم جدید
        /// </summary>  
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<TicketItemDto>), 200)]
        public async Task<IActionResult> Post(TicketItemDto TicketItemDto)
        {

            var dto = await TicketItemService.InsertAdminAsyncDto(TicketItemDto);
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
            var dto = TicketItemService.DeleteDto(id);
            return Ok(dto);
        }
    }
}
