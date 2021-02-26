using SmsService.Example.Models.Create;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmsService.Example.Models
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Starting SmsService example app");

            var apiKey = Config.ApiKey;

            // use different api key if provided via console input
            Console.Write($"Enter your API key ({apiKey}): ");
            var apiKeyInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(apiKeyInput))
            {
                apiKey = apiKeyInput;
            }

            Console.WriteLine($"Using API key \"{apiKey}\"");


            // SEND SMS
            var smsService = new SmsService(Config.BaseAddressSmsService, Config.ApiKey);
            
            var sms = new Create.Sms() {
                From = "sms service test app",
                Reference = "reference",
                Text = "Hello world, dit is een sms bericht",
                SendAt= new DateTime(2021, 02, 26, 12, 0, 0),
                Priority = false,
                Recipients = new List<Recipient>() 
                { 
                    new Recipient() { Number = "0494010203", Reference = "recipient reference" }
                }
            };

            var result = await smsService.SendSmsAsync(sms);
        }
    }
}
