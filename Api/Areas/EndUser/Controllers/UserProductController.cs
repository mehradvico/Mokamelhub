using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Accounting.UserProductSrv.Dto;
using Application.Services.Accounting.UserProductSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.EndUser.Controllers
{
    /// <summary>
    /// دریافت مقدار کیف پول
    /// </summary>
    ///
    [Area("EndUser")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProductController : ControllerBase
    {
        private readonly long _currentUserId;
        private readonly IUserProductService _userProductService;
        /// <summary>
        /// دریافت مقدار کیف پول
        /// </summary>
        ///
        public UserProductController(ICurrentUserHelper currentUserHelper, IUserProductService userProductService)
        {
            this._currentUserId = currentUserHelper.CurrentUser.UserId;
            this._userProductService = userProductService;
        }
        /// <summary>
        /// دریافت مقدار کیف پول
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResultDto<double>), 200)]
        public IActionResult Get([FromQuery] UserProductInputDto dto)
        {
            dto.UserId = this._currentUserId;
            var amount = _userProductService.SearchDto(dto);
            return Ok(amount);
        }

    }
}
