using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DigitalAssetsExpressExample
{
    public class DigitalAssetsExpressService
    {
        private readonly HttpClient _httpClient;

        public DigitalAssetsExpressService(string baseAddress, string apiKey)
        {
            // Use IHttpClientFactory (AddHttpClient) in real implementations
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress);
            _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
        }

        public async Task<JObject> Upload(Stream file, string fileName, string userId, bool generateThumbnail = false, bool returnThumbnailUrl = false, string thumbnailGeneratedCallbackUrl = null, int? thumbnailHeight = null)
        {
            var requestContent = new MultipartFormDataContent();

            var streamContent = new StreamContent(file);
            var contentDispositionHeader = $"form-data; name=\"file\"; filename=\"{fileName}\"";
            streamContent.Headers.Add("Content-Disposition", contentDispositionHeader);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            requestContent.Add(streamContent);

            requestContent.Add(new StringContent(userId), "userId");
            // The userId property can be used to specify the user who uploaded the file
            // The same userId is also needed when requesting URLs for or deleting an uploaded file

            requestContent.Add(new StringContent(generateThumbnail.ToString().ToLower()), "generateThumbnail");
            // More information about thumbnail generation can be found in the README

            requestContent.Add(new StringContent(returnThumbnailUrl.ToString().ToLower()), "returnThumbnailUrl");

            if (thumbnailHeight.HasValue)
            {
                requestContent.Add(new StringContent(thumbnailHeight.ToString()), "thumbnailHeight");
            }

            if (thumbnailGeneratedCallbackUrl != null)
            {
                requestContent.Add(new StringContent(thumbnailGeneratedCallbackUrl), "thumbnailGeneratedCallbackUrl");
            }

            var responseMessage = await _httpClient.PostAsync("api/mediafiles", requestContent);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Upload failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"Upload response ({(int)responseMessage.StatusCode}): {responseContent}");
            // Upload response (200): {"assetId":"R2XxaflXtTMMfTTZnD6E5P53","mediafileId":"I7hUQIUkZKX7OfQIPRLz8fXb","thumbnailGenerated":true,"fileName":"image.png","links":[{"rel":"download","href":"https://media-a.antwerpen.be/download/15/I/I7hUQIUkZKX7OfQIPRLz8fXb/image.png"}]}

            // Warning: The resulting download URL is permanent URL which is accessible by anyone. It might be necessary to conceal this URL from end users, depending on your use case.
            // It's possible to configure your application to only generate temporary URLs, or to setup Access Control Lists for your files using the Digital Assets API (not available through Digital Assets Express).
            // Please contact the ACPaaS team for more information if needed.

            return JObject.Parse(responseContent);
        }

        public async Task<JObject> GetUrl(string assetId, string mediafileId, string userId)
        {
            var responseMessage = await _httpClient.GetAsync($"api/assets/{assetId}/mediafiles/{mediafileId}/url?userid={userId}");
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"GetUrl failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"GetUrl response ({(int)responseMessage.StatusCode}): {responseContent}");
            // GetUrl response (200): {"mediafileDownloadUrl":"https://media-a.antwerpen.be/download/15/I/I1emYeYXTRMhjSAIEnZ7xmQf/image.png"}

            return JObject.Parse(responseContent);
        }

        public async Task<JObject> GetUrls(string assetId, string mediafileId, string userId)
        {
            var responseMessage = await _httpClient.GetAsync($"api/assets/{assetId}/mediafiles/{mediafileId}/urls?userid={userId}");
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"GetUrls failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"GetUrls response ({(int)responseMessage.StatusCode}): {responseContent}");
            // GetUrls response (200): {"mediafileDownloadUrl":"https://media-a.antwerpen.be/download/15/T/TpRSCyh6e3YujJsSlhZK0F2K/image.png","mediaFileViewUrl":"https://media-a.antwerpen.be/media/15/T/TpRSCyh6e3YujJsSlhZK0F2K/image.png","thumbnailUrl":"https://media-a.antwerpen.be/media/15/F/FXOLmfjL8QHdlJCuHG5tFQvJ/FXOLmfjL8QHdlJCuHG5tFQvJ.jpg"}

            return JObject.Parse(responseContent);
        }

        public async Task<JObject> GetThumbnailUrl(string assetId, string userId)
        {
            var responseMessage = await _httpClient.GetAsync($"api/assets/{assetId}/thumbnail/url?userid={userId}");
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"GetThumbnailUrl failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"GetThumbnailUrl response ({(int)responseMessage.StatusCode}): {responseContent}");
            // GetThumbnailUrl response (200): {"thumbnailUrl":"https://media-a.antwerpen.be/media/15/n/nUuHXXoh8jUXKftTv5yyRvIW/nUuHXXoh8jUXKftTv5yyRvIW.jpg"}

            return JObject.Parse(responseContent);
        }

        public async Task Delete(string assetId, string userId)
        {
            var responseMessage = await _httpClient.DeleteAsync($"api/assets/{assetId}?userid={userId}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception($"Delete failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"Delete response ({(int)responseMessage.StatusCode})");
            // Delete response (204)
        }

        public async Task<JObject> GetQuota()
        {
            var responseMessage = await _httpClient.GetAsync("apps/quota");
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"GetQuota failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"GetQuota response ({(int)responseMessage.StatusCode}): {responseContent}");
            // GetQuota response (200): {"appQuotaMb":"0","appDiskspaceUsedMb":"4843","quotaAvailableMb":"-4843"}

            return JObject.Parse(responseContent);
        }
    }
}
