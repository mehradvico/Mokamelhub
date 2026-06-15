using Newtonsoft.Json;

namespace Application.Services.Order.MerchantSrv.Dto.ZarinPalDto
{
    internal class ZarinPalRequestDto
    {
        [JsonProperty("MerchantID")]
        public string MerchantId { get; set; }

        [JsonProperty("Amount")]
        public int Amount { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Mobile")]
        public string Mobile { get; set; }

        [JsonProperty("CallbackURL")]
        public string CallbackUrl { get; set; }
    }


}
