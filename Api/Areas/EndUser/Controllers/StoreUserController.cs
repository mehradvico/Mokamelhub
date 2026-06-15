using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.ProductSrvs.StoreSrv.Dto;
using Application.Services.ProductSrvs.StoreUserSrv.Dto;
using Application.Services.StoreSrvs.StoreUserSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.EndUser.Controllers
{
    /// <summary>
    /// مدیریت فروشگاه ها
    /// </summary>
    ///
    [Area("EndUser")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreUserController : ControllerBase
    {
        private readonly IStoreUserService _storeUserService;
        private readonly ICurrentUserHelper _currentUserHelper;

        /// <summary>
        /// مدیریت فروشگاه ها
        /// </summary>
        ///
        public StoreUserController(IStoreUserService storeUserService, ICurrentUserHelper currentUserHelper)
        {
            _storeUserService = storeUserService;
            _currentUserHelper = currentUserHelper;
        }
        /// <summary>
        ///  فروشگاه ها 
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResultDto<List<StoreMinVDto>>), 200)]
        public async Task<IActionResult> Get()
        {
            var role = await _storeUserService.GetAllAsync(new StoreUserDto() { Active = true, UserId = _currentUserHelper.CurrentUser.UserId });
            return Ok(role);
        }

    }
}
