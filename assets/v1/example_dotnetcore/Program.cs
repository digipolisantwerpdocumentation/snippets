using System;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;

namespace AssetsExample
{
    class Program
    {
        private static AssetsService _service;

        static async Task Main(string apiKey = Config.ApiKey)
        {
            try
            {
                Console.WriteLine("Starting Assets API example app");
                Console.WriteLine($"Using API key \"{apiKey}\"");

                _service = new AssetsService(Config.BaseAddress, apiKey);

                var fileName = "image.png";
                var userId = "my-user-id";
                var secondUserId = "second-user-id";
                var thirdUserId = "third-user-id";

                var ticketResponse = (await _service.CreateUploadTicket(userId)).AssertSuccess();

                var ticketId = ticketResponse.SelectToken("response.items.item.ticket_id").ToString();
                var assetId = ticketResponse.SelectToken("response.items.item.asset_id").ToString();
                var mediafileId = ticketResponse.SelectToken("response.items.item.mediafile_id").ToString();

                using (var file = File.OpenRead(fileName))
                {
                    (await _service.UploadFile(ticketId, file, fileName, true, 20)).AssertSuccess();

                    // Get download URL
                    (await _service.GetMediafileView(assetId, userId, "download", mediafileId)).AssertSuccess();

                    // Get thumbnail URL (you should poll for the thumbnail URL as it's not instantly available)
                    (await _service.GetMediafileView(assetId, userId, "still", mediafileId)).AssertSuccess();

                    // Get download URL using another user ID (this will succeed because no ACL rules have been set on the mediafile)
                    (await _service.GetMediafileView(assetId, secondUserId, "download", mediafileId)).AssertSuccess();

                    // Set ACL rule so only specific users can access the mediafile
                    (await _service.SetMediafileAcl(mediafileId, userId, aclUserId: secondUserId)).AssertSuccess();

                    // Get download URL using another user ID (this will succeed because user has access through ACL rule)
                    // Note that the download URL is now temporary instead of permanent
                    (await _service.GetMediafileView(assetId, secondUserId, "download", mediafileId)).AssertSuccess();

                    // Get download URL using yet another user ID (this will fail because user has no access through ACL rule)
                    (await _service.GetMediafileView(assetId, thirdUserId, "download", mediafileId)).AssertFailure();

                    (await _service.DeleteAsset(assetId, userId)).AssertSuccess();

                    (await _service.GetQuota()).AssertSuccess();
                }
            }
            catch (Exception ex)
            {
                Console.Write($"Something when wrong: {ex.Message}");
            }
        }
    }

    public static class AssetsResultExtensions
    {
        public static JObject AssertSuccess(this JObject result)
        {
            result.GetRequestResult().Should().Be("success");

            return result;
        }

        public static JObject AssertFailure(this JObject result)
        {
            result.GetRequestResult().Should().Be("error");

            return result;
        }

        static string GetRequestResult(this JObject result)
        {
            return result.SelectToken("response.header.request_result").ToString();
        }
    }
}
