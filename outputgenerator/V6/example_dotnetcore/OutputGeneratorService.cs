using Newtonsoft.Json;
using OutputGeneratorExample.models;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OutputGeneratorExample
{
    public class OutputGeneratorService
    {
        public OutputGeneratorService(string baseAddress, string apiKey)
        {
            _baseAddress = baseAddress;
            _apiKey = apiKey;
        }

        private readonly string _baseAddress;
        private readonly string _apiKey;

        private HttpClient GethttpClient(bool automaticRedirect = true)
        {
            // Use Dependency injection/IHttpClientFactory (AddHttpClient) in real implementations
            
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            if (!automaticRedirect)
            {
                httpClientHandler.AllowAutoRedirect = false;
            }

            var httpClient = new HttpClient(httpClientHandler);

            httpClient.BaseAddress = new Uri(_baseAddress);
            httpClient.DefaultRequestHeaders.Add("ApiKey", _apiKey);

            return httpClient;
        }

        public async Task<Stream> GeneratePDFFromWordTemplate(string templateFile, string dataFile)
        {
            var requestContent = new MultipartFormDataContent();

            // TEMPLATE DATA
            var jsonContent = CreateByteArrayContentFromFile(dataFile);

            jsonContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            requestContent.Add(jsonContent, "data", "data.json");

            // TEMPLATE
            var templateContent = CreateByteArrayContentFromFile(templateFile);

            templateContent.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            requestContent.Add(templateContent, "wordTemplateData", "output.docx");

            requestContent.Add(new StringContent("pdf"), "resultType");

            requestContent.Add(new StringContent("false"), "async");


            // GENERATE DOCUMENT - excecute POST call
            var httpClient = GethttpClient();
            var response = await httpClient.PostAsync($"generator/directWordGeneration", requestContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Document generation failed ({(int)response.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"POST generate document response ({(int)response.StatusCode}): {responseContent}");

            var docGenerationResult = JsonConvert.DeserializeObject<DocumentGenerationResult>(responseContent, new JsonSerializerSettings());


            // GET DOCUMENT            
            response = await httpClient.GetAsync($"download?name={docGenerationResult.value.uploadUri}&target=upload", HttpCompletionOption.ResponseHeadersRead);
            
            if (!response.IsSuccessStatusCode)            
            {
                responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Document download failed ({(int)response.StatusCode}): {responseContent}");
            }

            return await response.Content.ReadAsStreamAsync();            
        }


        public async Task<Stream> GenerateAsyncPDFFromWordTemplate(string templateFile, string dataFile)
        {
            var requestContent = new MultipartFormDataContent();

            // TEMPLATE DATA
            var jsonContent = CreateByteArrayContentFromFile(dataFile);

            jsonContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            requestContent.Add(jsonContent, "data", "data.json");

            // TEMPLATE
            var templateContent = CreateByteArrayContentFromFile(templateFile);

            templateContent.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            requestContent.Add(templateContent, "wordTemplateData", "output.docx");

            requestContent.Add(new StringContent("pdf"), "resultType");

            requestContent.Add(new StringContent("true"), "async");


            // GENERATE DOCUMENT - expected response: status 202 - string with reference to generating/generated document
            var httpClient = GethttpClient(false);
            var response = await httpClient.PostAsync($"generator/directWordGeneration", requestContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Document generation failed ({(int)response.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"POST async generate document response ({(int)response.StatusCode}): {responseContent}");

            string docReference = JsonConvert.DeserializeObject<string>(responseContent, new JsonSerializerSettings());


            // GET DOCUMENT STATUS 
            // this part can be done by a different background process to not block a request 
            // during the document generation and download
            Stream docStream = null;

            if (!string.IsNullOrWhiteSpace(docReference))
            {
                bool documentGenerated = false;

                // get documentId
                var documentId = docReference.Split("/").Last();
                
                while (!documentGenerated)
                {
                    // expected response: 
                    // status 200 - string with status of generating/generated document
                    // status 303 - redirect with reference to generating document.
                    //              location header contains link. With automaticRedirect on, the response is the document itself
                    var statusResponse = await httpClient.GetAsync($"generator/task/result/d67e8e15-77cb-4d18-9b14-d29a89023431");

                    var statusResponseContent = await statusResponse.Content.ReadAsStringAsync();

                    Console.WriteLine($"GET document status response ({(int)statusResponse.StatusCode}): {statusResponseContent}");

                    if (statusResponse.StatusCode == System.Net.HttpStatusCode.RedirectMethod)
                    {
                        // document redirect information
                        var docRedirectResult = JsonConvert.DeserializeObject<DocumentRedirectResult>(statusResponseContent, new JsonSerializerSettings());
                        
                        // GET DOCUMENT            
                        response = await httpClient.GetAsync($"download?name={docRedirectResult.uploadUri}&target=upload", HttpCompletionOption.ResponseHeadersRead);

                        if (!response.IsSuccessStatusCode)
                        {
                            responseContent = await response.Content.ReadAsStringAsync();
                            throw new Exception($"Document download failed ({(int)response.StatusCode}): {responseContent}");
                        }

                        docStream = await response.Content.ReadAsStreamAsync();
                        documentGenerated = true;
                    }
                    else if (!statusResponse.IsSuccessStatusCode)
                    {
                        throw new Exception($"Get document status failed ({(int)statusResponse.StatusCode}): {statusResponseContent}");
                    }
                    else
                    {
                        var docGenerationResult = JsonConvert.DeserializeObject<DocumentGenerationAsyncResult>(statusResponseContent, new JsonSerializerSettings());

                        if (docGenerationResult.status.ToLower() == "failed")
                        {
                            throw new Exception($"Document generation failed: {statusResponseContent}");
                        }
                        else {
                            await Task.Delay(200);
                        }
                    }
                }
            }

            return docStream;
        }
        
        private ByteArrayContent CreateByteArrayContentFromFile(string file)
        {
            byte[] fileData = File.ReadAllBytes(file);
            ByteArrayContent bytes = new ByteArrayContent(fileData, 0, fileData.Count());
            return bytes;
        }

    }
}
