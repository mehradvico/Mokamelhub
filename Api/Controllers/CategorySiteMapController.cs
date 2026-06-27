using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Common.Helpers;
using Application.Services.CategorySrv.Dto;
using Application.Services.CategorySrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با دسته بندی ها
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategorySiteMapController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private const string SiteUrl = "https://mokamelhub.com";

        /// <summary>
        /// مرتبط با سایت مپ دسته بندی ها
        /// </summary>
        public CategorySiteMapController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        /// <summary>
        /// سایت مپ دسته بندی ها - JSON
        /// </summary>
        [HttpGet]
        [CustomOutputCache(CacheTypeEnum.CategorySiteMap)]
        [ProducesResponseType(typeof(BaseResultDto<List<CategorySiteMapDto>>), 200)]
        public IActionResult Get()
        {
            var list = _categoryService.GetSiteMap();
            return Ok(list);
        }

        /// <summary>
        /// سایت مپ دسته بندی ها - XML
        /// </summary>
        [HttpGet("xml")]
        [CustomOutputCache(CacheTypeEnum.CategorySiteMap)]
        public IActionResult GetXml()
        {
            var result = _categoryService.GetSiteMap() as BaseResultDto<List<CategorySiteMapDto>>;

            var categories = result?.Data ?? new List<CategorySiteMapDto>();

            var urls = categories
                .Where(x =>
                    !string.IsNullOrWhiteSpace(x.Label) &&
                    !x.Label.Equals("products", StringComparison.OrdinalIgnoreCase)
                )
                .Select(x =>
                    SitemapXmlHelper.CreateUrl(
                        $"{SiteUrl}/products?category={Uri.EscapeDataString(x.Label)}",
                        x.UpdateDate
                    )
                );

            var xml = SitemapXmlHelper.BuildUrlSet(urls);

            return SitemapXmlHelper.Xml(xml);
        }
    }
}