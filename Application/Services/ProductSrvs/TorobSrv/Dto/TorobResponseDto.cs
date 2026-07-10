using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.TorobSrv.Dto
{
    public sealed class TorobResponseDto
    {
        [JsonPropertyName("api_version")]
        public string ApiVersion { get; set; } = "torob_api_v3";

        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("max_pages")]
        public int MaxPages { get; set; }

        [JsonPropertyName("products")]
        public List<TorobProductDto> Products { get; set; } = new();
    }
}
