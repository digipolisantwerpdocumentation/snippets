using EmailService.Example.Models.Create;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailService.Example.Models
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Starting EmailService example app");

            var apiKey = Config.ApiKey;

            // use different api key if provided via console input
            Console.Write($"Enter your API key ({apiKey}): ");
            var apiKeyInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(apiKeyInput))
            {
                apiKey = apiKeyInput;
            }

            Console.WriteLine($"Using API key \"{apiKey}\"");


            // SEND EMAIL
            var emailService = new EmailService(Config.BaseAddressEmailService, Config.ApiKey);
            
            var sms = new Create.Email() {
                From = "test@digipolis.be",
                Reference = "reference",
                Subject = "test e-mail",
                Html = "<html xmlns=\"http://www.w3.org/1999/xhtml\"><head></head><body><p>Dit is een html bericht</p></body></html>",
                SendAt= new DateTime(2021, 02, 26, 12, 0, 0),
                Priority = false,
                Recipients = new List<Recipient>() 
                { 
                    new Recipient() { Email = "testrecipient@digipolis.be", Reference = "recipient reference" }
                },
                Attachments = new List<Attachment>(),
    			InlineImages = new List<InlineImage>()
            };

            var result = await emailService.SendEmailAsync(sms);
        }
    }
}
