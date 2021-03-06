using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WcmExample
{
    public class WcmService
    {
        private readonly HttpClient _httpClient;

        public WcmService(string baseAddress, string apiKey, string tenant)
        {
            // Use IHttpClientFactory (AddHttpClient) in real implementations
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress);
            _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
            _httpClient.DefaultRequestHeaders.Add("tenant", tenant);
        }

        public async Task<JObject> CreateContentItem(JObject content)
        {
            HttpContent requestContent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync("content", requestContent);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"CreateContentItem failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"CreateContentItem response ({(int)responseMessage.StatusCode}): {responseContent}");
            // CreateContentItem response (201): {"_id":"6037bc5215a804d1f07652d6","fields":{"test":"Some test"},"meta":{"contentType":{"_id":"5b921066534a07e22c43aece","versions":[],"fields":[{"_id":"test","validation":{"required":false},"type":"text","label":"test","operators":[{"label":"equals","value":"equals"},{"label":"contains","value":"i"},{"label":"starts with","value":"^"},{"label":"ends with","value":"$"}],"dataType":"string","indexed":false,"multiLanguage":false,"options":[],"max":1,"min":1,"taxonomyLists":[],"uuid":"27357f73-91ea-4e0c-89f2-f1a0d079f7d6"}],"meta":{"label":"testtype","description":"test type","safeLabel":"testtype","lastEditor":"5a002a269062ab6b7212d2dd","canBeFiltered":true,"deleted":false,"hitCount":0,"taxonomy":{"tags":[],"fieldType":"Taxonomy","available":[]},"lastModified":"2018-09-07T05:45:10.602Z","created":"2018-09-07T05:45:10.602Z"},"uuid":"39ea4178-3fa1-4d8d-b2f0-3a4285c3de2c","__v":0},"publishDate":"2020-10-20T11:00:00.000Z","label":"TestCodeSnippet","status":"PUBLISHED","safeLabel":"test_code_snippet","lastEditor":"111111111111111111111111","firstPublished":"2021-02-25T15:03:46.604Z","parents":{"views":[],"content":[]},"deleted":false,"hasDetail":false,"activeLanguages":["NL"],"hitCount":0,"hasScheduled":false,"published":true,"lastModified":"2021-02-25T15:03:46.604Z","created":"2021-02-25T15:03:46.604Z","taxonomy":{"tags":[],"dataType":"taxonomy","available":[]},"slug":{"NL":"test-code-snippet","multiLanguage":true}},"uuid":"a22ec337-ac77-4fc2-8eb0-3bc12d716daf","__v":0}

            return JObject.Parse(responseContent);
        }

        public async Task<JObject> GetContentItem(string uuid)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["lang"] = "NL";
            query["populate"] = "true";

            var responseMessage = await _httpClient.GetAsync($"content/{uuid}?{query}");
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"GetContentItem failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"GetContentItem response ({(int)responseMessage.StatusCode}): {responseContent}");
            // {"_id":"6037bc5215a804d1f07652d6","fields":{"test":"Some test"},"meta":{"publishDate":"2020-10-20T11:00:00.000Z","label":"TestCodeSnippet","status":"PUBLISHED","safeLabel":"test_code_snippet","firstPublished":"2021-02-25T15:03:46.604Z","parents":{"views":[],"content":[]},"deleted":false,"hasDetail":false,"activeLanguages":["NL"],"hitCount":0,"hasScheduled":false,"published":true,"lastModified":"2021-02-25T15:03:46.604Z","created":"2021-02-25T15:03:46.604Z","taxonomy":{"tags":[]},"slug":"test-code-snippet","historyRef":"381f124a-178e-4e13-b746-e6383d282985","contentType":"testtype","contentTypeUuid":"39ea4178-3fa1-4d8d-b2f0-3a4285c3de2c"},"uuid":"a22ec337-ac77-4fc2-8eb0-3bc12d716daf"}

            return JObject.Parse(responseContent);
        }

        public async Task<JObject> DeleteContentItem(string uuid)
        {
            var responseMessage = await _httpClient.DeleteAsync($"content/{uuid}");
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"DeleteContentItem failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"DeleteContentItem response ({(int)responseMessage.StatusCode})");
            // 

            return JObject.Parse(responseContent);
        }
    }
}
