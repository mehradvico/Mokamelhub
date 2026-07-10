using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.TorobSrv.Dto
{
    public sealed class TorobRequestDto
    {
        [JsonPropertyName("page")]
        public int? Page { get; set; }

        [JsonPropertyName("sort")]
        public string Sort { get; set; }

        [JsonPropertyName("page_urls")]
        public List<string> PageUrls { get; set; }

        [JsonPropertyName("page_uniques")]
        public List<string> PageUniques { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtraFields { get; set; }
    }
}
