using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotificationPreferences.Example.Models
{
    public class NotificationPreferencesService
    {
        public NotificationPreferencesService(string baseAddress, string apiKey)
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


        public async Task<Get.ContextPreferenceHALResponse> GetContextPreferencesAsync()
        {
            try
            {
                Console.WriteLine($"NotificationPreferencesService GET context preferences");

                var httpClient = GethttpClient();
                if (!httpClient.DefaultRequestHeaders.Any(x => x.Key.ToLowerInvariant() == CorrelationHeader.Key.ToLowerInvariant()))
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(CorrelationHeader.Key, CorrelationHeader.Default);
                }

                var response = await httpClient.GetAsync($"contextpreferences");

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"GET context preferences failed ({(int)response.StatusCode}): {responseContent}");
                }

                Console.WriteLine($"GET context preferences response ({(int)response.StatusCode}): {responseContent}");

                var contextPreferences = JsonConvert.DeserializeObject<Models.Get.ContextPreferenceHALResponse>(responseContent, new JsonSerializerSettings());
                return contextPreferences;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error NotificationPreferencesService - GetContextPreferencesAsync: {ex}");
                throw;
            }
        }

        public async Task<bool> CreateContextPreferenceAsync(Create.ContextPreference contextPreference)
        {
            try
            {
                Console.WriteLine($"NotificationPreferencesService create context preference for context {contextPreference.ContextName}");

                var httpClient = GethttpClient();
                if (!httpClient.DefaultRequestHeaders.Any(x => x.Key.ToLowerInvariant() == CorrelationHeader.Key.ToLowerInvariant()))
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(CorrelationHeader.Key, CorrelationHeader.Default);
                }

                var postContent = new StringContent(JsonConvert.SerializeObject(contextPreference));
                var response = await httpClient.PostAsync($"contextpreferences", postContent);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Create context preference failed ({(int)response.StatusCode}): {responseContent}");
                }

                Console.WriteLine($"POST create context preference response ({(int)response.StatusCode}): {responseContent}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error NotificationPreferencesService - CreateContextPreferenceAsync: {ex}");
                throw;
            }
        }
    }
}
