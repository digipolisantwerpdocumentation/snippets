using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace EventHandlerAdminExample
{
    class Program
    {
        private static EventHandlerAdminService _service;

        static async Task Main(string apiKey = Config.ApiKey, string namespaceOwnerKey = Config.NamespaceOwnerKey, string subscriptionOwnerKey = Config.SubscriptionOwnerKey, string adminOwnerKey = Config.AdminOwnerKey, string namespaceName = Config.Namespace, string topicName = Config.Topic, string subscriptionName = Config.Subscription, bool createNamespace = Config.CreateNamespace)
        {
            try
            {
                Console.WriteLine("Starting Event Handler Admin example app");
                Console.WriteLine($"Using API key \"{apiKey}\"");
                Console.WriteLine($"Using namespace owner key \"{namespaceOwnerKey}\"");
                Console.WriteLine($"Using subscription owner key \"{subscriptionOwnerKey}\"");
                Console.WriteLine($"Using namespace \"{namespaceName}\"");
                Console.WriteLine($"Using topic \"{topicName}\"");

                _service = new EventHandlerAdminService(Config.BaseAddress, apiKey, namespaceName, namespaceOwnerKey, subscriptionOwnerKey, adminOwnerKey);

                if (createNamespace)
                {
                    Console.WriteLine($"Using admin owner key \"{adminOwnerKey}\"");

                    await _service.CreateNamespace();
                }

                await _service.CreateTopic(topicName);
                await _service.CreateSubscription(topicName, subscriptionName, "http://localhost/some-subscription-endpoint");

                var message = JObject.FromObject(new { someProperty = "some event message" });
                await _service.Publish(topicName, message);

                await _service.DeleteSubscription(subscriptionName);
                await _service.DeleteTopic(topicName);

                if (createNamespace)
                {
                    await _service.DeleteNamespace(namespaceName);
                }
            }
            catch (Exception ex)
            {
                Console.Write($"Something went wrong: {ex.Message}");
            }
        }
    }
}
