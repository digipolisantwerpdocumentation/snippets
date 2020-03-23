using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventHandlerAdminExample
{
    public class EventHandlerAdminService
    {
        private readonly HttpClient _httpClient;
        private readonly string _namespaceName;
        private readonly string _namespaceOwnerKey;
        private readonly string _subscriptionOwnerKey;
        private readonly string _adminOwnerKey;

        public EventHandlerAdminService(string baseAddress, string apiKey, string namespaceName, string namespaceOwnerKey, string subscriptionOwnerKey)
        {
            // Use IHttpClientFactory (AddHttpClient) in real implementations
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress);
            _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);

            _namespaceName = namespaceName;
            _namespaceOwnerKey = namespaceOwnerKey;
            _subscriptionOwnerKey = subscriptionOwnerKey;
        }

        /// <summary>
        /// Alternative constructor with adminOwnerKey parameter (not for regular consumers)
        /// </summary>
        public EventHandlerAdminService(string baseAddress, string apiKey, string namespaceName, string namespaceOwnerKey, string subscriptionOwnerKey, string adminOwnerKey)
        {
            // Use IHttpClientFactory (AddHttpClient) in real implementations
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress);
            _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);

            _namespaceName = namespaceName;
            _namespaceOwnerKey = namespaceOwnerKey;
            _subscriptionOwnerKey = subscriptionOwnerKey;
            _adminOwnerKey = adminOwnerKey;
        }

        public async Task<JObject> CreateNamespace(string[] headerWhitelist = null)
        {
            var body = JObject.FromObject(new
            {
                name = _namespaceName,
                owner = _namespaceOwnerKey,
                headerWhitelist = headerWhitelist
            });

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "namespaces");

            requestMessage.Headers.Add("owner-key", _adminOwnerKey);
            requestMessage.Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

            var responseMessage = await _httpClient.SendAsync(requestMessage);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Create namespace failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"Create namespace response ({(int)responseMessage.StatusCode}): {responseContent}");
            // Create namespace response (201): {"name":"my-namespace","owner":"myNamespaceOwnerKey","headerWhitelist":null}

            return JObject.Parse(responseContent);
        }

        public async Task DeleteNamespace(string name)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"namespaces/{name}");

            requestMessage.Headers.Add("owner-key", _namespaceOwnerKey);

            var responseMessage = await _httpClient.SendAsync(requestMessage);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"Delete namespace failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"Delete namespace response ({(int)responseMessage.StatusCode})");
            // Delete namespace response (200)
        }

        public async Task<JObject> CreateTopic(string name)
        {
            var body = JObject.FromObject(new
            {
                name = name
            });

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"namespaces/{_namespaceName}/topics");

            requestMessage.Headers.Add("owner-key", _namespaceOwnerKey);
            requestMessage.Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

            var responseMessage = await _httpClient.SendAsync(requestMessage);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Create topic failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"Create topic response ({(int)responseMessage.StatusCode}): {responseContent}");
            // Create topic response (201): {"name":"my-topic","namespace":"my-namespace"}

            return JObject.Parse(responseContent);
        }

        public async Task DeleteTopic(string name)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"namespaces/{_namespaceName}/topics/{name}");

            requestMessage.Headers.Add("owner-key", _namespaceOwnerKey);

            var responseMessage = await _httpClient.SendAsync(requestMessage);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"Delete topic failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"Delete topic response ({(int)responseMessage.StatusCode})");
            // Delete topic response (200)
        }

        public async Task<JObject> CreateSubscription(string topicName, string name, string url)
        {
            var body = JObject.FromObject(new
            {
                name = name,
                topic = topicName,
                config = new
                {
                    push = new
                    {
                        url = url
                    }
                }
            });

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"namespaces/{_namespaceName}/subscriptions");

            requestMessage.Headers.Add("owner-key", _subscriptionOwnerKey);
            requestMessage.Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

            var responseMessage = await _httpClient.SendAsync(requestMessage);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Create subscription failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"Create subscription response ({(int)responseMessage.StatusCode}): {responseContent}");
            // Create subscription response (201): {"name":"my-subscription","namespace":"my-namespace","owner":"mySubscriptionOwnerKey","topic":"my-topic","config":{"maxConcurrentDeliveries":1,"retries":{"firstLevelRetries":{"enabled":true,"retries":3,"onFailure":"error"},"secondLevelRetries":{"enabled":false,"retries":10,"ttl":600,"onFailure":"error"}},"push":{"pushType":"http","httpVerb":"POST","authentication":{"type":"none","kafka":{},"basic":{},"apikey":{},"oauth":{}},"url":"http://localhost/some-subscription-endpoint"},"restartAfterStop":{"enabled":false,"delayInMinutes":30}},"updated":"2020-03-23T14:15:13.982Z","created":"2020-03-23T14:15:13.983Z","status":"active"}

            return JObject.Parse(responseContent);
        }

        public async Task DeleteSubscription(string name)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"namespaces/{_namespaceName}/subscriptions/{name}");

            requestMessage.Headers.Add("owner-key", _subscriptionOwnerKey);

            var responseMessage = await _httpClient.SendAsync(requestMessage);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"Delete subscription failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"Delete subscription response ({(int)responseMessage.StatusCode})");
            // Delete subscription response (200)
        }

        public async Task Publish(string topicName, JObject content)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"namespaces/{_namespaceName}/topics/{topicName}/publish");

            requestMessage.Headers.Add("owner-key", _namespaceOwnerKey);
            requestMessage.Content = new StringContent(content.ToString(), Encoding.UTF8, "application/json");

            var responseMessage = await _httpClient.SendAsync(requestMessage);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"Publish failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"Publish response ({(int)responseMessage.StatusCode})");
            // Publish response (204)
        }
    }
}
