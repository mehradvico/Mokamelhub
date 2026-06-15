using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.ProductLangSrv.Dto;
using Application.Services.Language.ProductLangSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت زبان محصولات
    /// </summary>
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductLanguageController : ControllerBase
    {
        private readonly IProductLangService productLangService;
        /// <summary>
        /// مدیریت زبان محصولات
        /// </summary>
        /// <param name="productLangService"></param>
        public ProductLanguageController(IProductLangService productLangService)
        {
            this.productLangService = productLangService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<ProductLangDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var product = await productLangService.FindAsyncDto(id);
            return Ok(product);
        }
        /// <summary>
        /// جسنجو
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ProductLangSearchDto), 200)]
        public IActionResult Get([FromQuery] ProductLangInputDto dto)
        {
            var search = productLangService.searchDto(dto);
            return Ok(search);
        }
        /// <summary>
        /// اضافه و ویرایش آیتم
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<ProductLangDto>), 200)]
        public async Task<IActionResult> Post(ProductLangDto dto)
        {
            var product = await productLangService.InsertAndUpdateDto(dto);
            return Ok(product);
        }
        /// <summary>
        /// حذف آیتم (ItemId=ProductId)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(OtherLangDeleteDto), 200)]
        public IActionResult Delete(OtherLangDeleteDto dto)
        {
            var delete = productLangService.DeleteDto(dto);
            return Ok(delete);
        }

    }
}
