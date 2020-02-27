using System;
using System.IO;
using System.Threading.Tasks;

namespace DigitalAssetsExpressExample
{
    class Program
    {
        private static DigitalAssetsExpressService _service;

        static async Task Main(string apiKey = Config.ApiKey)
        {
            try
            {
                Console.WriteLine("Starting Digital Assets Express example app");
                Console.WriteLine($"Using API key \"{apiKey}\"");

                _service = new DigitalAssetsExpressService(Config.BaseAddress, apiKey);

                var fileName = "image.png";
                var userId = "my-user-id";

                var uploadResult = await _service.Upload(File.OpenRead(fileName), fileName, userId, generateThumbnail: true, thumbnailHeight: 20);

                await _service.GetUrl(uploadResult.Value<string>("assetId"), uploadResult.Value<string>("mediafileId"), userId);
                await _service.GetUrls(uploadResult.Value<string>("assetId"), uploadResult.Value<string>("mediafileId"), userId);
                await _service.GetThumbnailUrl(uploadResult.Value<string>("assetId"), userId);

                await _service.Delete(uploadResult.Value<string>("assetId"), userId);

                await _service.GetQuota();
            }
            catch (Exception ex)
            {
                Console.Write($"Something when wrong: {ex.Message}");
            }
        }
    }
}
