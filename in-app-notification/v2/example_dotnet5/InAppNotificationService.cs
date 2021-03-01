using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InAppNotification.Example.Models
{
    public class InAppNotificationService
    {
        public InAppNotificationService(string baseAddress, string apiKey)
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

        public async Task<Get.InboxOverviewResponse> GetInboxOverviewAsync(string userId)
        {
            try
            {
                Console.WriteLine($"InAppNotificationService GET inbox overview");

                var httpClient = GethttpClient();
                if (!httpClient.DefaultRequestHeaders.Any(x => x.Key.ToLowerInvariant() == CorrelationHeader.Key.ToLowerInvariant()))
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(CorrelationHeader.Key, CorrelationHeader.Default);
                }

                var response = await httpClient.GetAsync($"inboxes/{userId}");

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"GET inbox overview failed ({(int)response.StatusCode}): {responseContent}");
                }

                Console.WriteLine($"GET inbox overview response ({(int)response.StatusCode}): {responseContent}");

                var inboxOverview = JsonConvert.DeserializeObject<Models.Get.InboxOverviewResponse>(responseContent, new JsonSerializerSettings());
                return inboxOverview;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error InAppNotificationService - GetInboxOverviewAsync: {ex}");
                throw;
            }
        }


        public async Task<Get.InboxMessagesHALResponse> GetInboxMessagesAsync(string userId)
        {
            try
            {
                Console.WriteLine($"InAppNotificationService GET inbox messages");

                var httpClient = GethttpClient();
                if (!httpClient.DefaultRequestHeaders.Any(x => x.Key.ToLowerInvariant() == CorrelationHeader.Key.ToLowerInvariant()))
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(CorrelationHeader.Key, CorrelationHeader.Default);
                }

                var response = await httpClient.GetAsync($"inboxes/{userId}/messages");

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"GET inbox messages failed ({(int)response.StatusCode}): {responseContent}");
                }

                Console.WriteLine($"GET inbox messages response ({(int)response.StatusCode}): {responseContent}");

                var inboxMessages = JsonConvert.DeserializeObject<Models.Get.InboxMessagesHALResponse>(responseContent, new JsonSerializerSettings());
                return inboxMessages;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error InAppNotificationService - GetInboxMessagesAsync: {ex}");
                throw;
            }
        }
    }
}
