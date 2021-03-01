using PushNotification.Example.Models.Create;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PushNotification.Example.Models
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Starting PushNotification example app");

            var apiKey = Config.ApiKey;

            // use different api key if provided via console input
            Console.Write($"Enter your API key ({apiKey}): ");
            var apiKeyInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(apiKeyInput))
            {
                apiKey = apiKeyInput;
            }

            Console.WriteLine($"Using API key \"{apiKey}\"");


            // SEND PUSH NOTIFICATION
            var pushNotificationService = new PushNotificationService(Config.BaseAddressPushNotification, Config.ApiKey);
            
            var pushNotification = new Create.PushNotification() {
                Title = "push notification",
                Body = "{\"action\":\"do action\"}",
                ApplicationId = "pushnotification-testapp",
                Action = "/action",
                Icon = "wave",
                Recipients = new List<Recipient>()
                {
                    new Recipient() { UserId = "aabc54f6d2b2afa8a7dc5d8b4568", Reference = "recipient reference" }
                },
            };

            var result = await pushNotificationService.SendPushNotificationAsync(pushNotification);
        }
    }
}
