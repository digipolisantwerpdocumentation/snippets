using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace WcmExample
{
    class Program
    {
        private static WcmService _service;

        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting WCM example app");

                _service = new WcmService(Config.BaseAddress, Config.ApiKey, Config.Tenant);

                var content = JObject.FromObject(new
                {
                    meta = new
                    {
                        activeLanguages = new JArray("NL"),
                        contentType = "5b921066534a07e22c43aece", // content type ID
                        publishDate = "2020-10-20T11:00:00.000Z",
                        slug = new
                        {
                            multiLanguage = true,
                            NL = "test-code-snippet",
                        },
                        label = "TestCodeSnippet",
                        status = "PUBLISHED",
                    },
                    fields = new
                    {
                        test = "Some test",
                    },
                });

                var result = await _service.CreateContentItem(content);
                await _service.GetContentItem(result.Value<string>("uuid"));
                await _service.DeleteContentItem(result.Value<string>("uuid"));
            }
            catch (Exception ex)
            {
                Console.Write($"Something went wrong: {ex.Message}");
            }
        }
    }
}
