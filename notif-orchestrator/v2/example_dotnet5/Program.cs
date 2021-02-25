using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationOrchestrator.Example.Models
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Starting Notification Orchestrator example app");

            var apiKey = Config.ApiKey;

            // use different api key if provided via console input
            Console.Write($"Enter your API key ({apiKey}): ");
            var apiKeyInput = Console.ReadLine();

            if (! string.IsNullOrWhiteSpace(apiKeyInput))
            {
                apiKey = apiKeyInput;
            }

            Console.WriteLine($"Using API key \"{apiKey}\"");


            // SEND NOTIFICATION
            var topicName = "<valid topic defined in notification orchestrator>";
            var notification = new Models.Notification()
            {
                Recipients= new List<Recipient>() {
                        new Recipient(){ 
                            TopicName = topicName
                        }
                    },
                SendAt = null,   // null = sent immediately; time = sent at designated time,
                Email = new Email
                {
                    Subject = $"Status update for topic: { topicName }",
                    From = "your-sender@antwerpen.be",
                    Html = "<b>${ message}</b>",
                    Text = "some message",
                }
            };

            var notificationOrchestratorService = new NotificationOrchestratorService(Config.BaseAddressOrchestrator, Config.ApiKey);
            var notificationId = await notificationOrchestratorService.SendNotificationToTopicAsync(notification);

            Console.WriteLine($"Notification sent, id: { notificationId }");

            // CREATE TOPIC
            var topic = new Topic() { 
                DefaultLanguage = "en",
                SupportedLanguages = { "en" },
                Name = "topic_name",
                DefaultChannel = "email",
                SupportedChannels = { "email" }
            };

            var notificationPreferenceAdmin = new NotificationPreferenceAdminService(Config.BaseAddressPreferenceAdmin, Config.ApiKey);
            var result = await notificationPreferenceAdmin.CreateTopicAsync(topic);
        }
    }
}
