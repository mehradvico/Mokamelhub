using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Services.Content.ContactUsGroupSrv.Dto;
using Application.Services.Content.ContactUsGroupSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با ارتباط با ما
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ContactUsGroupController : ControllerBase
    {
        private IContactUsGroupService ContactUsGroupService;
        /// <summary>
        /// مرتبط با ارتباط با ما
        /// </summary>
        public ContactUsGroupController(IContactUsGroupService ContactUsGroupService)
        {
            this.ContactUsGroupService = ContactUsGroupService;
        }
        /// <summary>
        /// جستجو
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(BaseSearchDto<ContactUsGroupDto>), 200)]
        public IActionResult Get([FromQuery] BaseInputDto dto)
        {
            dto.Available = true;
            var search = ContactUsGroupService.Search(dto);
            return Ok(search);
        }
    }
}
