using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.Content.DetailSrv.Dto;
using Application.Services.Content.DetailSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با مشخصات 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DetailController : ControllerBase
    {
        private IDetailService DetailService;
        /// <summary>
        /// مرتبط با مشخصات
        /// </summary>
        public DetailController(IDetailService DetailService)
        {
            this.DetailService = DetailService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="label">برچسب</param>
        /// <returns></returns>
        /// 
        [HttpGet("label/{label}")]
        [CustomOutputCache(CacheTypeEnum.DetailOne)]

        [ProducesResponseType(typeof(BaseResultDto<DetailVDto>), 200)]
        public async Task<IActionResult> Get(string label)
        {
            var Detail = await DetailService.GetByLabelAsync(label);
            return Ok(Detail);
        }


        /// <summary>
        /// جستجو
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(DetailSearchDto), 200)]
        [CustomOutputCache(CacheTypeEnum.DetailSearch)]
        public IActionResult Get([FromQuery] DetailInputDto dto)
        {
            var item = DetailService.Search(dto);
            return Ok(item);
        }
    }
}
