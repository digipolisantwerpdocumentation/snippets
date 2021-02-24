using System;
using System.Collections.Generic;

namespace NotificationOrchestrator.Example.Models
{
    class Program
    {
        static async void Main(string[] args)
        {
            Console.WriteLine("Starting Notification Orchestrator example app");

            var apiKey = Config.ApiKey;

            Console.Write($"Enter your API key ({apiKey}): ");
            var apiKeyInput = Console.ReadLine();

            if (apiKeyInput != "")
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

            var notificationOrchestratorService = new NotificationOrchestratorService(Config.BaseAddress, Config.ApiKey);
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

            var result = await notificationOrchestratorService.CreateTopicAsync(topic);
        }
    }
}
