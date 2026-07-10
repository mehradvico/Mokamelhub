using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.TorobSrv.Dto
{
    public sealed class TorobProductDto
    {
        [JsonPropertyName("page_unique")]
        public string PageUnique { get; set; }

        [JsonPropertyName("page_url")]
        public string PageUrl { get; set; }

        [JsonPropertyName("product_group_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ProductGroupId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("subtitle")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Subtitle { get; set; }

        [JsonPropertyName("current_price")]
        public long CurrentPrice { get; set; }

        [JsonPropertyName("old_price")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? OldPrice { get; set; }

        [JsonPropertyName("availability")]
        public bool Availability { get; set; }

        [JsonPropertyName("category_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CategoryName { get; set; }

        [JsonPropertyName("image_links")]
        public List<string> ImageLinks { get; set; } = new();

        [JsonPropertyName("spec")]
        public Dictionary<string, string> Spec { get; set; } = new();

        [JsonPropertyName("guarantee")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Guarantee { get; set; }

        [JsonPropertyName("short_desc")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ShortDescription { get; set; }

        [JsonPropertyName("date_added")]
        public string DateAdded { get; set; }

        [JsonPropertyName("date_updated")]
        public string DateUpdated { get; set; }
    }
}
