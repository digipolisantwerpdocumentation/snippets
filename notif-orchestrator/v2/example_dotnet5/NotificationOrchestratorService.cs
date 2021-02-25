using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotificationOrchestrator.Example.Models
{
    public class NotificationOrchestratorService
    {
        public NotificationOrchestratorService(string baseAddress, string apiKey)
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

        public async Task<string> SendNotificationToTopicAsync(Models.Notification notification)
        {
            try
            {
                Console.WriteLine($"NotificationOrchestratorService send notification {notification.Reference}: {JsonConvert.SerializeObject(notification)}");

                var httpClient = GethttpClient();
                if (!httpClient.DefaultRequestHeaders.Any(x => x.Key.ToLowerInvariant() == CorrelationHeader.Key.ToLowerInvariant()))
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(CorrelationHeader.Key, CorrelationHeader.Default);
                }

                var postContent = new StringContent(JsonConvert.SerializeObject(notification));
                var response = await httpClient.PostAsync($"notifications", postContent);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Send notifcation failed ({(int)response.StatusCode}): {responseContent}");
                }

                Console.WriteLine($"POST send notification response ({(int)response.StatusCode}): {responseContent}");

                var notificationResult = JsonConvert.DeserializeObject<Models.SendNotificationResult>(responseContent, new JsonSerializerSettings());

                return notificationResult.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"NotificationOrchestratorService: Error while sending notification with reference {notification.Reference}: {ex.ToString()}");
                throw;
            }
        }
    }
}
