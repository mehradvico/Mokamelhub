using Application.Common.Enumerable;
using Application.Services.Content.StaticPageSrv.Dto;
using Application.Services.Content.StaticPageSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با صفحات ثابت
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class StaticPageController : ControllerBase
    {
        private IStaticPageService StaticPageService;
        /// <summary>
        /// مرتبط با صفحات ثابت
        /// </summary>
        public StaticPageController(IStaticPageService StaticPageService)
        {
            this.StaticPageService = StaticPageService;
        }
        /// <summary>
        /// جستجو
        /// </summary>
        [HttpGet]
        [CustomOutputCache(CacheTypeEnum.StaticPage)]
        [ProducesResponseType(typeof(StaticPageVDto), 200)]
        public async Task<IActionResult> Get(string label)
        {
            var staticPage = await StaticPageService.GetByLabelAsync(label);
            return Ok(staticPage);
        }
    }
}
