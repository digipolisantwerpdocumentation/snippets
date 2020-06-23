using DigitalVaultExample.models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DigitalVaultExample
{
    public class DigitalVaultService
    {
        public DigitalVaultService(IServiceProvider serviceProvider, IMemoryCache memoryCache)
        {
            serviceProvider = _serviceProvider;
            _memoryCache = memoryCache;
        }

        private readonly IServiceProvider _serviceProvider;
        private readonly IMemoryCache _memoryCache;

        private const int MaxRequestAttempts = 5;
        private const int RequestDelayInSeconds = 5;

        private HttpClient _httpClient = null;
        private HttpClient DigitalVaultHttpClient
        {
            get {
                if (_httpClient == null)
                {
                    // Use Dependency injection/IHttpClientFactory (AddHttpClient) in real implementations

                    HttpClientHandler httpClientHandler = new HttpClientHandler();
                    _httpClient = new HttpClient(httpClientHandler);

                    _httpClient.BaseAddress = new Uri(Config.BaseAddress);
                }
                return _httpClient;
            }
        }

        public async Task<DocumentCheckResponse> CheckDocumentExists(string organizationUnitId, 
                                                                     string name, 
                                                                     DateTime referenceDate, 
                                                                     List<string> destinations,
                                                                     string bulkOperation)
        {
            DocumentCheck check = new DocumentCheck()
            {
                Name = name,
                Destinations = destinations,
                ReferenceDate = referenceDate,
                BulkOperation = bulkOperation
            };

            var serializedValue = JsonConvert.SerializeObject(check, new JsonSerializerSettings());
            HttpContent requestContent = new StringContent(serializedValue, Encoding.UTF8, "application/json");

            if (!DigitalVaultHttpClient.DefaultRequestHeaders.Contains("Dgp-UserContext"))
            {
                DigitalVaultHttpClient.DefaultRequestHeaders.Add("Dgp-UserContext", "document-provider");
            }

            var accessToken = await GetAccesToken(string.Empty);
            DigitalVaultHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await DigitalVaultHttpClient.PostAsync($"organizationunits/{organizationUnitId}/documents/check", requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Document check failed ({response.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"POST check document exists response ({(int)response.StatusCode}): {responseContent} ");

            return JsonConvert.DeserializeObject<DocumentCheckResponse>(responseContent);
        }

        public async Task<UploadResponse> UploadWithRetry(string organizationUnitId, 
                                                          UploadRootObject uploadObject)
        {
            var serializedValue = JsonConvert.SerializeObject(uploadObject, new JsonSerializerSettings());

            if (!DigitalVaultHttpClient.DefaultRequestHeaders.Contains("Dgp-UserContext"))
            {
                DigitalVaultHttpClient.DefaultRequestHeaders.Add("Dgp-UserContext", "document-provider");
            }

            var accessToken = await GetAccesToken(string.Empty);
            DigitalVaultHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            for (int i = 1; i <= MaxRequestAttempts; i++)
            {
                try
                {
                    HttpContent contentPost = new StringContent(serializedValue, Encoding.UTF8, "application/json");

                    var response = await DigitalVaultHttpClient.PostAsync($"organizationunits/{organizationUnitId}/documents", contentPost);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Upload for organisation unit {organizationUnitId} failed ({response.StatusCode}): {responseContent}");
                    }

                    Console.WriteLine($"POST upload document response ({(int)response.StatusCode}): {responseContent} ");

                    return JsonConvert.DeserializeObject<UploadResponse>(responseContent);
                }
                catch (Exception)
                {
                    await Task.Delay(new TimeSpan(0, 0, RequestDelayInSeconds));
                }
            }

            throw new Exception("Maximum upload attempts exceeded!");
        }
        
        private async Task<string> GetAccesToken(string scope)
        {
            var cacheKey = "DIGITALVAULT_ACCESSTOKEN";

            var tokenReply = _memoryCache.Get<TokenReply>(cacheKey);

            if (tokenReply == null)
            {
                var content = $"client_id={Config.OAuthClientId}&client_secret={Config.OAuthClientSecret}&grant_type=client_credentials{(String.IsNullOrWhiteSpace(scope) ? "" : $"&scope={scope}")}";

                var response = await DigitalVaultHttpClient.PostAsync("oauth2/token", new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded"));

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error retrieving token, response status code: " + response.StatusCode);
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                tokenReply = JsonConvert.DeserializeObject<TokenReply>(responseBody);

                var cacheExpiration = tokenReply.expires_in - 60;
                cacheExpiration = cacheExpiration > 0 ? cacheExpiration : 0;

                if (cacheExpiration > 0)
                {
                    _memoryCache.Set(cacheKey, tokenReply, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheExpiration) });
                }
            }

            return tokenReply.access_token;
        }
    }
}
