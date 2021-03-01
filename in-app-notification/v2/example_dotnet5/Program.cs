using System;
using System.Threading.Tasks;

namespace InAppNotification.Example.Models
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Starting InAppNotification example app");

            var apiKey = Config.ApiKey;

            // use different api key if provided via console input
            Console.Write($"Enter your API key ({apiKey}): ");
            var apiKeyInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(apiKeyInput))
            {
                apiKey = apiKeyInput;
            }

            Console.WriteLine($"Using API key \"{apiKey}\"");


            //  GET INBOX OVERVIEW
            var InAppNotificationService = new InAppNotificationService(Config.BaseAddressInAppNotification, Config.ApiKey);
            
            var inboxOverview = await InAppNotificationService.GetInboxOverviewAsync("user id");

            //  GET INBOX MESSAGES
            var inboxMessagesHALResponse = await InAppNotificationService.GetInboxMessagesAsync("user id");
        }
    }
}
