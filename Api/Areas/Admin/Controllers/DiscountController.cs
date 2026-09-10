using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.DiscountSrv.Dto;
using Application.Services.ProductSrvs.DiscountSrv.IFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت برندها
    /// </summary>
    ///
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class DiscountController : ControllerBase
    {
        private IDiscountService discountService;
        /// <summary>
        /// مدیریت برند ها
        /// </summary>
        ///
        public DiscountController(IDiscountService discountService)
        {
            this.discountService = discountService;
        }
        /// <summary>
        ///  اطلاعات آیتم 
        /// </summary>
        /// <param name="id">شناسه دسته بندی</param>
        /// <returns>
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<DiscountVDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {

            var role = await discountService.FindDetailAsync(id);
            return Ok(role);
        }
        /// <summary>
        ///  جستجو
        /// </summary>
        /// <returns></returns> 

        [HttpGet]
        [ProducesResponseType(typeof(DiscountSearchDto), 200)]
        public IActionResult Get([FromQuery] DiscountInputDto dto)
        {
            var searchDto = discountService.Search(dto);
            return Ok(searchDto);
        }
        /// <summary>
        /// آیتم جدید
        /// </summary>  
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<DiscountDto>), 200)]
        public async Task<IActionResult> Post(DiscountDto discountDto)
        {

            var dto = await discountService.InsertAsync(discountDto);
            return Ok(dto);
        }
        /// <summary>
        /// ویرایش آیتم
        /// </summary>

        [HttpPut]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public async Task<IActionResult> Put(DiscountDto discountDto)
        {

            var dto = await discountService.ActiveAsync(discountDto);
            return Ok(dto);
        }
        /// <summary>
        /// حذف آیتم
        /// </summary>
        ///
        [HttpDelete]
        [HttpPost("delete")]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public async Task<IActionResult> Delete([FromQuery] long id)
        {
            var dto = await discountService.DeleteAsync(id);
            return Ok(dto);
        }
    }
}
