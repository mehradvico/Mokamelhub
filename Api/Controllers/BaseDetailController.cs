using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.Setting.BaseDetailSrv.Dto;
using Application.Services.Setting.BaseDetailSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مدیریت مشخصات پایه
    /// </summary>
    ///
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BaseDetailController : ControllerBase
    {
        private IBaseDetailService BaseDetailService;
        /// <summary>
        /// مدیریت مشخصات پایه
        /// </summary>
        ///
        public BaseDetailController(IBaseDetailService BaseDetailService)
        {
            this.BaseDetailService = BaseDetailService;
        }
        /// <summary>
        ///  اطلاعات آیتم 
        /// </summary>
        /// <param name="label">برچسب</param>
        /// <returns>
        /// </returns>
        [HttpGet("{label}")]
        [CustomOutputCache(CacheTypeEnum.BaseDetail)]
        [ProducesResponseType(typeof(BaseResultDto<BaseDetailDto>), 200)]
        public async Task<IActionResult> Get(string label)
        {
            if (string.IsNullOrEmpty(label))
            {
                label = BaseDetailEnum.frontend.ToString();
            }
            var role = await BaseDetailService.GetDtoAsync(label);
            return Ok(role);
        }

    }
}
