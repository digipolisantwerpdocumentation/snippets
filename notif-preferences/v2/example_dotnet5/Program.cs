using System;
using System.Threading.Tasks;

namespace NotificationPreferences.Example.Models
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Starting Notification Preferences example app");

            var apiKey = Config.ApiKey;

            // use different api key if provided via console input
            Console.Write($"Enter your API key ({apiKey}): ");
            var apiKeyInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(apiKeyInput))
            {
                apiKey = apiKeyInput;
            }

            Console.WriteLine($"Using API key \"{apiKey}\"");


            // GET CONTEXT PREFERENCES
            var notificationPreferencesService = new NotificationPreferencesService(Config.BaseAddressNotificationPreference, Config.ApiKey);
            var preferences = await notificationPreferencesService.GetContextPreferencesAsync();

            Console.WriteLine($"Context preferences: { Newtonsoft.Json.JsonConvert.SerializeObject(preferences) }");

            // CREATE CONTEXT PREFERENCE
            var contextPreference = new Create.ContextPreference() {
                ContextName = "contextName",
                Channel = "Channel",
                UserId = "user id",
                Language = "nl"
            };

            var result = await notificationPreferencesService.CreateContextPreferenceAsync(contextPreference);
        }
    }
}
