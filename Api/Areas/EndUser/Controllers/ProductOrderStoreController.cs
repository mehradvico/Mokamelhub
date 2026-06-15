using Application.Common.Interface;
using Application.Services.Order.ProductOrderStoreOrderSrv.Dto;
using Application.Services.Order.ProductOrderStoreSrv.Dto;
using Application.Services.Order.ProductOrderStoreSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.EndUser.Controllers
{
    /// <summary>
    /// مدیریت  آیتم سفارش ها
    /// </summary>
    ///
    [Area("EndUser")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductOrderStoreController : ControllerBase
    {
        private readonly IProductOrderStoreService _productOrderStoreService;
        private readonly ICurrentUserHelper _currentUserHelper;

        /// <summary>
        /// مدیریت  آیتم سفارش ها
        /// </summary>
        ///
        public ProductOrderStoreController(IProductOrderStoreService productOrderStoreService, ICurrentUserHelper currentUserHelper)
        {
            this._productOrderStoreService = productOrderStoreService;
            this._currentUserHelper = currentUserHelper;
        }

        /// <summary>
        ///  جستجو
        /// </summary>
        /// <returns></returns> 
        [HttpGet()]
        [ProducesResponseType(typeof(ProductOrderStoreSearchDto), 200)]
        public IActionResult Get([FromQuery] ProductOrderStoreInputDto dto)
        {
            dto.UserId = _currentUserHelper.CurrentUser.UserId;
            var productOrderItems = _productOrderStoreService.Search(dto);
            return Ok(productOrderItems);
        }

    }
}
