using RestSharp;
using Utility.ExternalRequest.Iface;

namespace Utility.ExternalRequest.Service
{
    public class RestSharpApi : IRestSharpApi
    {
        private readonly static object _GetUsdLocker = new object();

        public string GetUsd(string label)
        {
            try
            {
                lock (_GetUsdLocker)
                {
                    var client = new RestClient("https://api.coingecko.com/");
                    var request = new RestRequest($"api/v3/simple/price?ids={label}&vs_currencies=usd&include_last_updated_at=true");
                    var response = client.ExecuteGet(request);
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content;

                    }
                    else
                    {
                        return null;
                    }
                }

            }
            catch { return null; }
        }
    }
}
