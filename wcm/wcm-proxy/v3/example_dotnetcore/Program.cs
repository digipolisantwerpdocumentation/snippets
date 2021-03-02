using System;
using System.Threading.Tasks;

namespace WcmProxyExample
{
    class Program
    {
        private static WcmProxyService _service;

        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting WCM Proxy example app");

                _service = new WcmProxyService(Config.BaseAddress, Config.ApiKey, Config.Tenant);

                var view = await _service.GetView(Config.ViewId);
                await _service.GetContentItem(view["data"].First.Value<string>("uuid"));
            }
            catch (Exception ex)
            {
                Console.Write($"Something went wrong: {ex.Message}");
            }
        }
    }
}
