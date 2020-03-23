using System;
using System.IO;
using System.Threading.Tasks;

namespace OutputGeneratorExample
{
    class Program
    {
        private static OutputGeneratorService _service;

        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting Output Generator example app");

                var apiKey = Config.ApiKey;

                Console.Write($"Enter your API key ({apiKey}): ");
                var apiKeyInput = Console.ReadLine();

                if (apiKeyInput != "")
                {
                    apiKey = apiKeyInput;
                }

                Console.WriteLine($"Using API key \"{apiKey}\"");

                _service = new OutputGeneratorService(Config.BaseAddress, apiKey);

                // generate document synchronically
                await GenerateWordDocumentFromTemplate();

                // generate document asynchronically
                await GenerateAsyncWordDocumentFromTemplate();
            }
            catch (Exception ex)
            {
                Console.Write($"Something went wrong: {ex.Message}");
            }
        }

        // SYNCHRONOUS
        static async Task GenerateWordDocumentFromTemplate()
        {
            var templateFile = $"_content/word_simple_template.docx";
            var dataFile = $"_content/word_simple_template_data.json";

            var documentStream = await _service.GeneratePDFFromWordTemplate(templateFile, dataFile);

            await SaveFile(documentStream, $"_content/generated/MyGeneratedDocument__{Guid.NewGuid().ToString()}.pdf");
        }

        // ASYNCHRONOUS
        static async Task GenerateAsyncWordDocumentFromTemplate()
        {
            var templateFile = $"_content/word_simple_template.docx";
            var dataFile = $"_content/word_simple_template_data.json";

            var documentStream = await _service.GenerateAsyncPDFFromWordTemplate(templateFile, dataFile);

            await SaveFile(documentStream, $"_content/generated/MyAsycGeneratedDocument_{Guid.NewGuid().ToString()}.pdf");
        }

        private static async Task SaveFile(Stream stream, string filePath)
        {
            DirectoryInfo dir = new DirectoryInfo("_content/generated");
            if (!dir.Exists) dir.Create();

            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                await File.WriteAllBytesAsync(filePath, ms.ToArray());
            }
        }
    }
}
