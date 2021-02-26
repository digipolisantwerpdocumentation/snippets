using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmsService.Example.Models
{
    public class SmsService
    {
        public SmsService(string baseAddress, string apiKey)
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


        public async Task<bool> SendSmsAsync(Create.Sms sms)
        {
            try
            {
                Console.WriteLine($"SmsService send SMS to { String.Join(", ", sms.Recipients.Select(r => r.Number)) }");

                var httpClient = GethttpClient();
                if (!httpClient.DefaultRequestHeaders.Any(x => x.Key.ToLowerInvariant() == CorrelationHeader.Key.ToLowerInvariant()))
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(CorrelationHeader.Key, CorrelationHeader.Default);
                }

                var postContent = new StringContent(JsonConvert.SerializeObject(sms));
                var response = await httpClient.PostAsync($"sms", postContent);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Send SMS failed ({(int)response.StatusCode}): {responseContent}");
                }

                Console.WriteLine($"POST send SMS response ({(int)response.StatusCode}): {responseContent}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error SmsService - SendSmsAsync: {ex}");
                throw;
            }
        }
    }
}
