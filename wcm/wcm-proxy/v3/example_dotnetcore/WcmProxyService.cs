using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace WcmProxyExample
{
    public class WcmProxyService
    {
        private readonly HttpClient _httpClient;

        public WcmProxyService(string baseAddress, string apiKey, string tenant)
        {
            // Use IHttpClientFactory (AddHttpClient) in real implementations
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress);
            _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
            _httpClient.DefaultRequestHeaders.Add("tenant", tenant);
        }

        public async Task<JObject> GetView(string uuid)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["uuid"] = uuid;
            query["lang"] = "nl";
            query["populate"] = "true";
            query["skip"] = "0";
            query["limit"] = "10";

            var responseMessage = await _httpClient.GetAsync($"views?{query}");
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"GetView failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"GetView response ({(int)responseMessage.StatusCode}): {responseContent}");

            return JObject.Parse(responseContent);
        }

        public async Task<JObject> GetContentItem(string uuid)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["uuid"] = uuid;
            query["lang"] = "nl";
            query["populate"] = "true";

            var responseMessage = await _httpClient.GetAsync($"content?{query}");
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"GetContentItem failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"GetContentItem response ({(int)responseMessage.StatusCode}): {responseContent}");

            return JObject.Parse(responseContent);
        }
    }
}
