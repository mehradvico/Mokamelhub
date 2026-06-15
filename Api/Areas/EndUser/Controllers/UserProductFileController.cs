using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.ProductSrvs.ProductFileSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.EndUser.Controllers
{
    /// <summary>
    /// فایل های محصول
    /// </summary>
    ///
    [Area("EndUser")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserProductFileController : ControllerBase
    {
        private readonly long? _currentUserId;
        private readonly IProductFileService _productFileService;
        /// <summary>
        /// فایل های محصول
        /// </summary>
        ///
        public UserProductFileController(ICurrentUserHelper currentUserHelper, IProductFileService productFileService)
        {
            this._currentUserId = currentUserHelper?.CurrentUser?.UserId;
            this._productFileService = productFileService;
        }

        /// <summary>
        /// فایل های محصول
        /// </summary>
        [HttpGet("Id")]
        [ProducesResponseType(typeof(BaseResultDto<double>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var dto = await _productFileService.GetForUserAsync(id, _currentUserId);
            return Ok(dto);
        }

    }
}
