using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PushNotification.Example.Models
{
    public class PushNotificationService
    {
        public PushNotificationService(string baseAddress, string apiKey)
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


        public async Task<bool> SendPushNotificationAsync(Create.PushNotification pushNotification)
        {
            try
            {
                Console.WriteLine($"PushNotificationService send push notification to { String.Join(", ", pushNotification.Recipients.Select(r => r.UserId)) }");

                var httpClient = GethttpClient();
                if (!httpClient.DefaultRequestHeaders.Any(x => x.Key.ToLowerInvariant() == CorrelationHeader.Key.ToLowerInvariant()))
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(CorrelationHeader.Key, CorrelationHeader.Default);
                }

                var postContent = new StringContent(JsonConvert.SerializeObject(pushNotification));
                var response = await httpClient.PostAsync($"notifications", postContent);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Send push notification failed ({(int)response.StatusCode}): {responseContent}");
                }

                Console.WriteLine($"POST send push notification response ({(int)response.StatusCode}): {responseContent}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error PushNotificationService - SendPushNotificationAsync: {ex}");
                throw;
            }
        }
    }
}
