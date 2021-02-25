using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotificationOrchestrator.Example.Models
{
    public class NotificationPreferenceAdminService
    {
        public NotificationPreferenceAdminService(string baseAddress, string apiKey)
        {
            _baseAddress = baseAddress;
            _apiKey = apiKey;
        }

        private readonly string _baseAddress;
        private readonly string _apiKey;

        private HttpClient GethttpClient(bool automaticRedirect = true)
        {
            // Use Dependency injection/IHttpClientFactory (AddHttpClient) in real implementations

            HttpClientHandler httpClientHandler = new HttpClientHandler();
            if (!automaticRedirect)
            {
                httpClientHandler.AllowAutoRedirect = false;
            }

            var httpClient = new HttpClient(httpClientHandler);

            httpClient.BaseAddress = new Uri(_baseAddress);
            httpClient.DefaultRequestHeaders.Add("ApiKey", _apiKey);

            return httpClient;
        }

        public async Task<bool> CreateTopicAsync(Topic topic)
        {
            try
            {
                Console.WriteLine($"NotificationPreferenceAdminService create topic {topic.Name}");

                var httpClient = GethttpClient();
                if (!httpClient.DefaultRequestHeaders.Any(x => x.Key.ToLowerInvariant() == CorrelationHeader.Key.ToLowerInvariant()))
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(CorrelationHeader.Key, CorrelationHeader.Default);
                }

                var postContent = new StringContent(JsonConvert.SerializeObject(topic));
                var response = await httpClient.PostAsync($"topics", postContent);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Create topic failed ({(int)response.StatusCode}): {responseContent}");
                }

                Console.WriteLine($"POST create topic response ({(int)response.StatusCode}): {responseContent}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"NotificationPreferenceAdminService: Error while creating topic {topic.Name}: {ex.ToString()}");
                throw;
            }
        }
    }
}
