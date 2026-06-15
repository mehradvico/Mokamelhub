using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.StoreUserSrv.Dto;
using Application.Services.StoreSrvs.StoreUserSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت کاربران فروشگاه
    /// </summary>
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreUserController : ControllerBase
    {
        private readonly IStoreUserService _storeUserService;

        /// <summary>
        /// مدیریت کاربران فروشگاه
        /// </summary>
        ///
        public StoreUserController(IStoreUserService storeUserService)
        {
            this._storeUserService = storeUserService;
        }
        /// <summary>
        ///  کاربران فروشگاه 
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public async Task<IActionResult> Get([FromQuery] StoreUserDto storeUser)
        {
            var role = await _storeUserService.GetAllAsync(storeUser);
            return Ok(role);
        }
        /// <summary>
        ///  کاربران فروشگاه 
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public async Task<IActionResult> Post(StoreUserDto storeUser)
        {
            var role = await _storeUserService.InsertAsync(storeUser);
            return Ok(role);
        }
        /// <summary>
        ///  کاربران فروشگاه 
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpDelete]
        [ProducesResponseType(typeof(BaseResultDto), 200)]
        public async Task<IActionResult> Delete(StoreUserDto storeUser)
        {
            var role = await _storeUserService.RemoveAsync(storeUser);
            return Ok(role);
        }

    }
}
