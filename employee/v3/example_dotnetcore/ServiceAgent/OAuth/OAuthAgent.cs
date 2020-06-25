using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeExample.ServiceAgent.OAuth
{
    public class OAuthAgent : IOAuthAgent
    {
        private readonly HttpClient _client;
        private readonly IMemoryCache _memoryCache;

        public OAuthAgent(HttpClient client, IMemoryCache memoryCache)
        {
            _client = client;
            _memoryCache = memoryCache;
        }

        public async Task<string> ReadOrRetrieveAccessToken()
        {
            var tokenReplyResult = await RetrieveTokenReply(Config.OAuthClientId, Config.OAuthClientSecret, string.Empty, Config.OAuthTokenEndpoint);

            if (tokenReplyResult == null)
                throw new Exception($"Unable to retrieve token reply for agent.");

            return tokenReplyResult.access_token;
        }

        private async Task<TokenReply> RetrieveTokenReply(string clientID, string clientSecret, string scope, string tokenEndpoint)
        {
            var cacheKey = "EMPLOYEE_ACCESSTOKEN";

            var tokenReply = _memoryCache.Get<TokenReply>(cacheKey);

            if (tokenReply == null)
            {
                var content = $"client_id={clientID}&client_secret={clientSecret}&grant_type=client_credentials{(String.IsNullOrWhiteSpace(scope) ? "" : $"&scope={scope}")}";

                using (StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded"))
                {
                    using (var response = await _client.PostAsync(tokenEndpoint, stringContent))
                    {
                        if (!response.IsSuccessStatusCode)
                            throw new Exception("Error retrieving OAuth access token, response status code: " + response.StatusCode);
                        try
                        {
                            using (var stream = await response.Content.ReadAsStreamAsync())
                            {
                                tokenReply = await System.Text.Json.JsonSerializer.DeserializeAsync<TokenReply>(stream);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error parsing OAuth access token: " + ex.Message, ex);
                        }
                    }
                }

                var cacheExpiration = tokenReply.expires_in - 60;
                cacheExpiration = cacheExpiration > 0 ? cacheExpiration : 0;

                if (cacheExpiration > 0)
                {
                    _memoryCache.Set(cacheKey, tokenReply, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheExpiration) });
                }
            }

            return tokenReply;
        }
    }
}
