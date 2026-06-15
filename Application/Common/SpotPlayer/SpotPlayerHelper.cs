using Application.Common.Dto.SpotPlayer;
using Application.Common.Interface;
using Newtonsoft.Json;
using RestSharp;

namespace Application.Common.SpotPlayer
{
    public class SpotPlayerHelper : ISpotPlayer
    {
        public SpotPlayerResponseDto GetToken(string course, string name, string payload, string texts, bool test = false)
        {
            var requestDto = new SpotPlayerRequestDto(course, name, payload, texts, test);
            var client = new RestClient("https://panel.spotplayer.ir");
            var request = new RestRequest($"license/edit/", method: Method.Post);
            request.AddJsonBody(requestDto);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("$API", "ZRqQV0H3YQGPbToxlv6OvFTpiQM0gw==");
            request.AddHeader("$LEVEL", "-1");
            var response = client.ExecutePost(request);
            if (response.IsSuccessStatusCode)
            {
                var item = JsonConvert.DeserializeObject<SpotPlayerResponseDto>(response.Content);
                return item;

            }
            else
            {
                return null;
            }
        }
    }
}
