using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Helpers
{
    public static class SitemapXmlHelper
    {
        public static ContentResult Xml(string xml)
        {
            return new ContentResult
            {
                Content = xml,
                ContentType = "application/xml; charset=utf-8",
                StatusCode = StatusCodes.Status200OK
            };
        }

        public static string BuildUrlSet(IEnumerable<string> urls)
        {
            return $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9"">
{string.Join(Environment.NewLine, urls)}
</urlset>";
        }

        public static string CreateUrl(string loc, DateTime updateDate)
        {
            var lastmod = updateDate.Year >= 2000
                ? $"    <lastmod>{updateDate:yyyy-MM-dd}</lastmod>{Environment.NewLine}"
                : "";

            return $@"  <url>
    <loc>{WebUtility.HtmlEncode(loc)}</loc>
{lastmod}  </url>";
        }
    }
}
