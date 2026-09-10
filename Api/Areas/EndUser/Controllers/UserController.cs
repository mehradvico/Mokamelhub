using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Accounting.UserSrv.Iface;
using Application.Services.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.EndUser.Controllers
{
    /// <summary>
    /// مدیریت کاربران
    /// </summary>
    ///
    [Area("EndUser")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private IUserService userService;
        private ICurrentUserHelper _currentUserHelper;
        /// <summary>
        /// مدیریت کاربران
        /// </summary>
        ///
        public UserController(IUserService user, ICurrentUserHelper currentUserHelper)
        {
            this.userService = user;
            this._currentUserHelper = currentUserHelper;
        }

        /// <summary>
        ///  اطلاعات آیتم 
        /// </summary>
        /// <param name="id">شناسه</param>
        /// <returns>
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<UserDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            // A user may only ever fetch their own profile — the route id is ignored
            // on purpose so a client can't read another user's data by changing it.
            var item = await userService.FindAsyncDto(_currentUserHelper.CurrentUser.UserId);
            return Ok(item);
        }

        /// <summary>
        /// ویرایش آیتم
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public async Task<IActionResult> Put(UserDto userDto)
        {
            var currentUserId = _currentUserHelper.CurrentUser.UserId;

            // Force the target to the caller's own account and re-assert the
            // account's current role/lock state so a self-service edit can never
            // retarget another user or self-escalate/self-unlock via this endpoint.
            var current = await userService.FindAsyncDto(currentUserId);
            if (!current.IsSuccess)
                return Ok(current);

            userDto.Id = currentUserId;
            userDto.RoleId = current.Data.RoleId;
            userDto.Locked = current.Data.Locked;

            var dto = userService.UpdateDto(userDto);
            return Ok(dto);
        }
    }
}
