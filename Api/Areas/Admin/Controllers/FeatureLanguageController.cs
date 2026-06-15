using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.FeatureLangSrv.Dto;
using Application.Services.Language.FeatureLangSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت زبان ویژگی ها
    /// </summary>
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class FeatureLanguageController : ControllerBase
    {
        private readonly IFeatureLangService featureLangService;
        /// <summary>
        /// مدیریت زبان ویژگی ها
        /// </summary>
        /// 

        public FeatureLanguageController(IFeatureLangService featureLangService)
        {
            this.featureLangService = featureLangService;
        }
        /// <summary>
        /// اطلاعات آیتم
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResultDto<FeatureLangDto>), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var item = await featureLangService.FindAsyncDto(id);
            return Ok(item);
        }
        /// <summary>
        /// جستجو
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(FeatureLangSearchDto), 200)]
        public IActionResult Get([FromQuery] FeatureLangInputDto dto)
        {
            var item = featureLangService.SearchDto(dto);
            return Ok(item);
        }

        /// <summary>
        /// اضافه و ویرایش آیتم
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultDto<FeatureLangDto>), 200)]
        public async Task<IActionResult> Post(FeatureLangDto dto)
        {
            var item = await featureLangService.InsertAndUpdateAsyncDto(dto);
            return Ok(item);
        }
        /// <summary>
        /// حذف آیتم(ItemId=FeatureId)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(OtherLangDeleteDto), 200)]
        public IActionResult Delete(OtherLangDeleteDto dto)
        {
            var item = featureLangService.DeleteDto(dto);
            return Ok(item);
        }

    }
}
