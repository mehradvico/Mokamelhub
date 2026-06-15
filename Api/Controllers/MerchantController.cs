using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Services.Order.MerchantSrv.Dto;
using Application.Services.Order.MerchantSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با بنر ها
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class MerchantController : ControllerBase
    {
        private IMerchantService MerchantService;
        /// <summary>
        /// مرتبط با درگاه بانکی
        /// </summary>
        public MerchantController(IMerchantService MerchantService)
        {
            this.MerchantService = MerchantService;
        }


        /// <summary>
        /// جستجو
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(BaseSearchDto<MerchantVDto>), 200)]
        public IActionResult Get([FromQuery] BaseInputDto dto)
        {
            dto.Available = true;
            var model = MerchantService.Search(dto);
            return Ok(model);
        }
    }
}
