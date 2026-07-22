using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.EndUser.Controllers
{
    /// <summary>
    /// کاربر جاری
    /// </summary>
    ///
    [Area("EndUser")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrentUserController : ControllerBase
    {
        private readonly ICurrentUserHelper _currentUserHelper;
        /// <summary>
        /// کاربر جاری
        /// </summary>
        ///
        public CurrentUserController(ICurrentUserHelper currentUserHelper)
        {
            _currentUserHelper = currentUserHelper;
        }
        /// <summary>
        ///  دریافت 
        /// </summary>

        [HttpGet]
        [ProducesResponseType(typeof(BaseResultDto<CurrentUserDto>), 200)]
        public IActionResult Get()
        {
            var currentUser = _currentUserHelper.CurrentUser;
            if (currentUser == null)
            {
                return Ok(new BaseResultDto(false));
            }
            else
            {
                return Ok(new BaseResultDto<CurrentUserDto>(true, currentUser));
            }
        }
    }
}
