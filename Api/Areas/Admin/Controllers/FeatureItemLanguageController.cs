using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.FeatureItemLangSrv.Dto;
using Application.Services.Language.FeatureItemLangSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت زبان آیتم ویژگی ها
    /// </summary>
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class FeatureItemLanguageController : ControllerBase
    {
        private readonly IFeatureItemLangService featureItemLangService;
        /// <summary>
        /// مدیریت زبان آیتم ویژگی ها
        /// </summary>
        /// 

        public FeatureItemLanguageController(IFeatureItemLangService featureItemLangService)
        {
            this.featureItemLangService = featureItemLangService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<FeatureItemLangDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var item = await featureItemLangService.FindAsyncDto(id);
            return Ok(item);
        }
        /// <summary>
        /// جستجو
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(FeatureItemLangSearchDto), 200)]
        public IActionResult Get([FromQuery] FeatureItemLangInputDto dto)
        {
            var item = featureItemLangService.SearchDto(dto);
            return Ok(item);
        }

        /// <summary>
        /// اضافه و ویرایش آیتم
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<FeatureItemLangDto>), 200)]
        public async Task<IActionResult> Post(FeatureItemLangDto dto)
        {
            var item = await featureItemLangService.InsertAndUpdateAsyncDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// حذف آیتم(ItemId=FeatureItemId)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(OtherLangDeleteDto), 200)]
        public IActionResult Delete(OtherLangDeleteDto dto)
        {
            var item = featureItemLangService.DeleteDto(dto);
            return Ok(item);
        }

    }
}
