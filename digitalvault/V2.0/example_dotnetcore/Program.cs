using DigitalVaultExample.models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DigitalVaultExample
{
    class Program
    {
        private static DigitalVaultService _service;

        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting Digital Vault example app");

                if (string.IsNullOrWhiteSpace(Config.OAuthClientId) || string.IsNullOrWhiteSpace(Config.OAuthClientSecret))
                {
                    throw new Exception("Define configuration variables for OAUTH");
                }

                // setup DI
                IServiceProvider serviceProvider = ServiceHelper.InitializeServices();                
                _service = (DigitalVaultService)serviceProvider.GetService(typeof(DigitalVaultService));

                //----------------------------------------------------------------------
                // define upload data

                // documents are uploaded to organisational units; set the correct name of an existing unit
                var organisationalUnit = "8";

                // user's reference known within the organisational unit (ex. national number, unique id, employee number, ...)
                var destination = "2020010112345";

                // name of bulk operation; all documents uploaded with the same bulk operation name are considered as belonging together
                var bulkoperationName = $"bulkupload_test_{ DateTime.Now.ToString("yyyyMMdd")}";

                // filename
                var documentName = "testdocument.pdf";

                // reference date
                DateTime referenceDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

                //----------------------------------------------------------------------

                // check if document exists
                var exists = await CheckDocumentExists(organisationalUnit, destination, bulkoperationName, documentName, referenceDate);

                // upload document
                if (!exists)
                {
                    await UploadDocument(organisationalUnit, destination, bulkoperationName, documentName, referenceDate);
                }
            }
            catch (Exception ex)
            {
                Console.Write($"Something went wrong: {ex.Message}");
            }
        }

        // CHECK FILE
        static async Task<bool> CheckDocumentExists(string organisationalUnit, string destination, 
                                                    string bulkOperationName, string documentName,
                                                    DateTime referenceDate)
        {
            // compose list with destination names (~user references) known within the organisational unit (ex. national numbers);
            // The combination organisational unit and destination name must be unique within the Digital Vault;
            // Each combination is linked to a single user's vault; 
            // If the combination is not yet present in the Digital Vault, this is added as a new record unlinked to any vault.
            // If a user's vault is later linked to the unlinked organisation unit - destination name combination, the uploaded
            // document will become available in the user's vault. 
            // The linking of vaults with destination names is an administration task and not done by client applications!
            List<string> destinations = new List<string>() { destination };

            var documentCheckResponse = await _service.CheckDocumentExists(organisationalUnit, documentName, 
                                                                           referenceDate, destinations,
                                                                           bulkOperationName);
            return documentCheckResponse.Exist;
        }

        private static async Task UploadDocument(string organisationalUnit, string destinationName,
                                                 string bulkOperationName, string documentName,
                                                 DateTime referenceDate)
        {
            var externalName = $"Mijn testdocument van {DateTime.Now.ToShortDateString()}";

            var fileContent = GetFile("testdocument.pdf");
            var fileContentAsBase64String = Convert.ToBase64String(fileContent);

            var upload = new UploadRootObject()
            {
                Name = documentName,
                ExternalName = externalName,
                Content = fileContentAsBase64String,

                // avoid min-date "01/01/0001 00:00:00"
                ReferenceDate = referenceDate,

                // only alphanumeric characters, underscores and dashes are allowed (name is used as route parameter in Vault UI)
                BulkOperation = bulkOperationName,

                Metadata = new List<Metadata>(),
                Destinations = new List<Destination>()
            };

            var destination = new Destination()
            {
                Name = destinationName,
                AutoCommit = true,  // make document visible to the user; if false, an administrator must commit the document manually later on
                NotificationNeeded = false,
                Tags = new List<string>() { "test", destinationName }
            };
            upload.Destinations.Add(destination);

            // Document type - must be known within the vault;
            // Defining a document type and the related metadata for an organisational unit is an administrative task
            // and can be done with the Digital Vault admin UI in the near future.
            upload.DocumentType = "normal";

            // Metadata - the list of possible metadata fields is related to the document type
            // A metadata-field must be defined with the document type before it can be used during upload;
            // If a required metadata-field or values isn't supplied during upload, an 500-error is returned;
            // Therefore if you don't know if a value is always present, don't define a metadata-field as required
            upload.Metadata.AddRange(new List<Metadata> {
                        new Metadata("gebruikers_id", "123456"),
                        new Metadata("tags", "001234"),
                        new Metadata("documentnaam", externalName),
                        new Metadata("refertejaar_maand", DateTime.Now.ToString("yyyy/MM")),
                        new Metadata("naam", destination.Name),
                        new Metadata("voornaam", "test"),
                        new Metadata("rijksregisternummer", destinationName),
                        new Metadata("personeelsnummer", "001234"),
                        new Metadata("vestigingsplaats", "locatie 1"),
                        new Metadata("afreken_eenheid", "snip-it"),
                        new Metadata("werkgever", "Snippet Inc."),
                        new Metadata("bestandsnaam", documentName)
                    }); ;

            var response = await _service.UploadWithRetry(organisationalUnit, upload);
        }

        private static byte[] GetFile(string fileName)
        {
            // path starts from bin-folder
            var filePath = $"../../../_content/{fileName}";
            
            try
            {
                return File.ReadAllBytes(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Can't retrieve file {fileName} at location {filePath}", ex);
            }
        }
    }
}
