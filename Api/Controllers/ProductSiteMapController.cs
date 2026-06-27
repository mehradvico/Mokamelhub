using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Common.Helpers;
using Application.Services.ProductSrv.Dto;
using Application.Services.ProductSrvs.ProductSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// مرتبط با سایت مپ محصولات
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductSiteMapController : ControllerBase
    {
        private readonly IProductService productService;
        private const string SiteUrl = "https://mokamelhub.com";

        /// <summary>
        /// مرتبط با سایت مپ محصولات
        /// </summary>
        public ProductSiteMapController(IProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// سایت مپ محصولات - JSON
        /// </summary>
        [HttpGet]
        [CustomOutputCache(CacheTypeEnum.ProductSiteMap)]
        [ProducesResponseType(typeof(BaseResultDto<List<ProductSiteMapDto>>), 200)]
        public IActionResult Get()
        {
            var list = productService.GetSiteMap();
            return Ok(list);
        }

        /// <summary>
        /// سایت مپ محصولات - XML
        /// </summary>
        [HttpGet("xml")]
        [CustomOutputCache(CacheTypeEnum.ProductSiteMap)]
        public IActionResult GetXml()
        {
            var result = productService.GetSiteMap() as BaseResultDto<List<ProductSiteMapDto>>;

            var products = result?.Data ?? new List<ProductSiteMapDto>();

            var urls = products
                .Where(x => !string.IsNullOrWhiteSpace(x.Label))
                .Select(x =>
                    SitemapXmlHelper.CreateUrl(
                        $"{SiteUrl}/product/{Uri.EscapeDataString(x.Label)}",
                        x.UpdateDate
                    )
                );

            var xml = SitemapXmlHelper.BuildUrlSet(urls);

            return SitemapXmlHelper.Xml(xml);
        }
    }
}