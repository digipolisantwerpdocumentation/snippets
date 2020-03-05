using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AssetsExample
{
    public class AssetsService
    {
        private readonly HttpClient _httpClient;

        public AssetsService(string baseAddress, string apiKey)
        {
            // Use IHttpClientFactory (AddHttpClient) in real implementations
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress);
            _httpClient.DefaultRequestHeaders.Add("apikey", apiKey);
        }

        public async Task<JObject> CreateUploadTicket(string userId)
        {
            var requestContent = new MultipartFormDataContent();

            requestContent.Add(new StringContent(userId), "user_id");
            // The user_id property can be used to specify the user who uploaded the file
            // The same user ID is also needed for other calls concerning the same file

            requestContent.Add(new StringContent("true"), "isprivate");
            requestContent.Add(new StringContent("true"), "is_downloadable");
            requestContent.Add(new StringContent("true"), "published");

            var response = await _httpClient.PostAsync("upload/ticket/create", requestContent);
            // CreateUploadTicket response (200): <?xml version="1.0" encoding="UTF-8"?><response xmlns:dc="http://purl.org/dc/elements/1.1/"><header><item_count>1</item_count><item_count_total>1</item_count_total><item_offset>0</item_offset><request_class>mediamosa_rest_call_upload_ticket_create</request_class><request_matched_method>POST</request_matched_method><request_matched_uri>/upload/ticket/create</request_matched_uri><request_result>success</request_result><request_result_description></request_result_description><request_result_id>601</request_result_id><request_uri>[POST] upload/ticket/create</request_uri><version>3.7.0.2203-rc1dev</version><request_process_time>0.0375</request_process_time></header><items><item><action>https://media-a.antwerpen.be/mediafile/upload?upload_ticket=W9Dhe4sU9F8CAYYirmFwFvK0</action><uploadprogress_url>https://media-a.antwerpen.be/uploadprogress?id=9980626</uploadprogress_url><asset_id>u1mkXRZCcJNEaXb5JMtqrH3K</asset_id><mediafile_id>O2RWXgWpZXdXHUmVYQ8afrRv</mediafile_id><ticket_id>W9Dhe4sU9F8CAYYirmFwFvK0</ticket_id><progress_id>9980626</progress_id><server_id>131</server_id></item></items></response>

            return await ParseResult(response, "CreateUploadTicket");
        }

        public async Task<JObject> UploadFile(string ticketId, Stream stream, string fileName, bool createThumbnail, int? thumbnailSize)
        {
            var requestContent = new MultipartFormDataContent();

            requestContent.Add(new StringContent(ticketId), "upload_ticket");
            requestContent.Add(new StringContent(createThumbnail.ToString().ToLower()), "create_still");

            if (thumbnailSize.HasValue)
            {
                requestContent.Add(new StringContent(thumbnailSize.ToString()), "size");
            }

            using (var streamContent = new StreamContent(stream))
            {
                // Filename normally doesn't contain double quotes but remove them just to be sure
                var headerFileName = fileName.Replace("\"", "");
                var contentDispositionHeader = $"form-data; name=\"file\"; filename=\"{headerFileName}\"";

                // UTF-8 conversion to handle special characters in filename
                contentDispositionHeader = new string(Encoding.UTF8.GetBytes(contentDispositionHeader).Select(b => (char)b).ToArray());

                streamContent.Headers.Add("Content-Disposition", contentDispositionHeader);

                streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                requestContent.Add(streamContent);

                var response = await _httpClient.PostAsync("mediafile/upload", requestContent);
                // UploadFile response (200): <?xml version="1.0" encoding="UTF-8"?><response xmlns:dc="http://purl.org/dc/elements/1.1/"><header><item_count>0</item_count><item_count_total>0</item_count_total><item_offset>0</item_offset><request_class>mediamosa_rest_call_asset_mediafile_upload</request_class><request_matched_method>POST</request_matched_method><request_matched_uri>/mediafile/upload</request_matched_uri><request_result>success</request_result><request_result_description></request_result_description><request_result_id>601</request_result_id><request_uri>[POST] mediafile/upload</request_uri><version>3.7.0.2203-rc1dev</version><request_process_time>0.5721</request_process_time></header><items/></response>

                return await ParseResult(response, "UploadFile");
            };
        }

        public async Task<JObject> GetMediafileView(string assetId, string userId, string responseType, string mediafileId)
        {
            var responseMessage = await _httpClient.GetAsync($"asset/{assetId}/view?user_id={userId}&response={responseType}&mediafile_id={mediafileId}");

            return await ParseResult(responseMessage, $"GetMediafileView (user_id={userId}, response={responseType})");
            // GetMediafileView (user_id=my-user-id, response=download) response (200): <?xml version="1.0" encoding="UTF-8"?><response xmlns:dc="http://purl.org/dc/elements/1.1/"><header><item_count>1</item_count><item_count_total>1</item_count_total><item_offset>0</item_offset><request_class>mediamosa_rest_call_media_view</request_class><request_matched_method>GET</request_matched_method><request_matched_uri>/asset/$asset_id/view</request_matched_uri><request_result>success</request_result><request_result_description></request_result_description><request_result_id>601</request_result_id><request_uri>[GET] asset/u1mkXRZCcJNEaXb5JMtqrH3K/view?user_id=my-user-id&amp;response=download&amp;mediafile_id=O2RWXgWpZXdXHUmVYQ8afrRv</request_uri><version>3.7.0.2203-rc1dev</version><request_process_time>0.0381</request_process_time></header><items><item><output>https://media-a.antwerpen.be/download/15/O/O2RWXgWpZXdXHUmVYQ8afrRv/image.png</output><content_type>image/png</content_type><ticket_id>O2RWXgWpZXdXHUmVYQ8afrRv</ticket_id></item></items></response>
        }

        public async Task<JObject> SetMediafileAcl(string mediafileId, string userId, string aclUserId = null, string aclGroupId = null, string aclDomain = null, string aclRealm = null)
        {
            var requestContent = new MultipartFormDataContent();

            requestContent.Add(new StringContent(userId), "user_id");

            if (aclUserId != null) {
                requestContent.Add(new StringContent(aclUserId), "acl_user_id");
            }

            if (aclGroupId != null) {
                requestContent.Add(new StringContent(aclGroupId), "acl_group_id");
            }

            if (aclDomain != null) {
                requestContent.Add(new StringContent(aclDomain), "acl_domain");
            }

            if (aclRealm != null) {
                requestContent.Add(new StringContent(aclRealm), "acl_realm");
            }

            var response = await _httpClient.PostAsync($"mediafile/{mediafileId}/acl", requestContent);
            // SetMediafileAcl response (200): <?xml version="1.0" encoding="UTF-8"?><response xmlns:dc="http://purl.org/dc/elements/1.1/"><header><item_count>1</item_count><item_count_total>1</item_count_total><item_offset>0</item_offset><request_class>mediamosa_rest_call_acl_mediafile_set_rights</request_class><request_matched_method>POST</request_matched_method><request_matched_uri>/mediafile/$mediafile_id/acl</request_matched_uri><request_result>success</request_result><request_result_description></request_result_description><request_result_id>601</request_result_id><request_uri>[POST] mediafile/O2RWXgWpZXdXHUmVYQ8afrRv/acl</request_uri><version>3.7.0.2203-rc1dev</version><request_process_time>0.0773</request_process_time></header><items><item><acl_user><value>second-user-id</value><result>success</result><result_id>601</result_id><result_description></result_description></acl_user></item></items></response>

            return await ParseResult(response, "SetMediafileAcl");
        }

        public async Task<JObject> DeleteAsset(string assetId, string userId)
        {
            var requestContent = new MultipartFormDataContent();

            requestContent.Add(new StringContent(userId), "user_id");
            requestContent.Add(new StringContent("cascade"), "delete");

            var response = await _httpClient.PostAsync($"asset/{assetId}/delete", requestContent);
            // DeleteAsset response (200): <?xml version="1.0" encoding="UTF-8"?><response xmlns:dc="http://purl.org/dc/elements/1.1/"><header><item_count>0</item_count><item_count_total>0</item_count_total><item_offset>0</item_offset><request_class>mediamosa_rest_call_asset_delete</request_class><request_matched_method>POST</request_matched_method><request_matched_uri>/asset/$asset_id/delete</request_matched_uri><request_result>success</request_result><request_result_description></request_result_description><request_result_id>601</request_result_id><request_uri>[POST] asset/u1mkXRZCcJNEaXb5JMtqrH3K/delete</request_uri><version>3.7.0.2203-rc1dev</version><request_process_time>0.057</request_process_time></header><items/></response>

            return await ParseResult(response, "DeleteAsset");
        }

        public async Task<JObject> GetQuota()
        {
            var response = await _httpClient.GetAsync("app/quota");
            // GetQuota response (200): <?xml version="1.0" encoding="UTF-8"?><response xmlns:dc="http://purl.org/dc/elements/1.1/"><header><item_count>1</item_count><item_count_total>1</item_count_total><item_offset>0</item_offset><request_class>mediamosa_rest_call_app_get_quota</request_class><request_matched_method>GET</request_matched_method><request_matched_uri>/app/quota</request_matched_uri><request_result>success</request_result><request_result_description></request_result_description><request_result_id>601</request_result_id><request_uri>[GET] app/quota</request_uri><version>3.7.0.2203-rc1dev</version><request_process_time>0.0093</request_process_time></header><items><item><app_quota_mb>0</app_quota_mb><app_diskspace_used_mb>4844.202246666</app_diskspace_used_mb><quota_available_mb>-4844.202246666</quota_available_mb></item></items></response>

            return await ParseResult(response, "GetQuota");
        }

        private async Task<JObject> ParseResult(HttpResponseMessage response, string method)
        {
            var responseXml = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{method} failed ({(int)response.StatusCode}): {responseXml}");
            }

            Console.WriteLine($"{method} response ({(int)response.StatusCode}): {responseXml}");

            // Parse XML to Json.NET JObject (consider deserializing to custom classes in real implementations)
            var xml = new XmlDocument();
            xml.LoadXml(responseXml);

            var parsedResult = JObject.Parse(JsonConvert.SerializeObject(xml));

            if (parsedResult.SelectToken("response.header.request_result").ToString() == "error")
            {
                // You should probably throw an exception in real implementations
                Console.WriteLine($"{method} failed: {parsedResult.SelectToken("response.header.request_result_description")}");
            }

            return parsedResult;
        }
    }
}
