using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.ProductSrvs.ProductReportSrv.Dto;
using Application.Services.ProductSrvs.ProductReportSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.EndUser.Controllers
{
    /// <summary>
    /// مدیریت گزارشات تخلف محصولات
    /// </summary>
    /// 
    [Area("EndUser")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductReportController : ControllerBase
    {
        private readonly IProductReportService _ProductReportService;
        private readonly ICurrentUserHelper _currentUser;
        public ProductReportController(IProductReportService ProductReportService, ICurrentUserHelper currentUser)
        {
            this._ProductReportService = ProductReportService;
            this._currentUser = currentUser;
        }
        /// <summary>
        /// آیتم جدید
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<ProductReportDto>), 200)]
        public async Task<IActionResult> Post(ProductReportDto dto)
        {
            dto.UserId = _currentUser.CurrentUser.UserId;
            var result = await _ProductReportService.InsertAsyncDto(dto);
            return Ok(result);
        }
    }
}
