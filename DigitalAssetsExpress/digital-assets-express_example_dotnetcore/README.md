# Digital Assets Express examples (.NET Core)

The Digital Assets Express API provides a simplified API to access the Digital Assets engine.

These code snippets and console app show how to upload, get and delete a file using the Digital Assets Express API.

## Links

- [Digital Assets engine on ACPaaS Portal](https://acpaas.digipolis.be/nl/product/digital-assets-engine)
- Digital Assets Express
  - [API Store ACC](https://api-store-a.antwerpen.be/#/org/acpaas/api/digitalassetsexpress/v1/documentation)
- Digital Assets
  - [API Store ACC](https://api-store-a.antwerpen.be/#/org/inuits/api/assets/v1/documentation)
  - [ACPaaS Wiki page](https://wiki.antwerpen.be/ACPAAS/index.php/Digital_Asset_express_engine)

## Code snippets

These snippets and other examples are available in [DigitalAssetsExpressService.cs](DigitalAssetsExpressService.cs).

First configure the base URL and API key:

```csharp
public DigitalAssetsExpressService(string baseAddress, string apiKey)
{
    // Use IHttpClientFactory (AddHttpClient) in real implementations
    _httpClient = new HttpClient();
    _httpClient.BaseAddress = new Uri(baseAddress);
    _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
}
```

### Upload file

`POST /api/mediafiles`

Response:

```json
{
  "assetId": "R2XxaflXtTMMfTTZnD6E5P53",
  "mediafileId": "I7hUQIUkZKX7OfQIPRLz8fXb",
  "thumbnailGenerated": true,
  "fileName": "image.png",
  "links": [{
      "rel": "download",
      "href": "https://media-a.antwerpen.be/download/15/I/I7hUQIUkZKX7OfQIPRLz8fXb/image.png"
    }
  ]
}
```

Example implementation:

```csharp
public async Task<JObject> Upload(Stream file, string fileName, string userId, bool generateThumbnail = false, bool returnThumbnailUrl = false, string thumbnailGeneratedCallbackUrl = null, int? thumbnailHeight = null)
{
    var requestContent = new MultipartFormDataContent();

    var streamContent = new StreamContent(file);
    var contentDispositionHeader = $"form-data; name=\"file\"; filename=\"{fileName}\"";
    streamContent.Headers.Add("Content-Disposition", contentDispositionHeader);
    streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
    requestContent.Add(streamContent);

    requestContent.Add(new StringContent(userId), "userId");
    // The userId property can be used to specify the user who uploaded the file
    // The same userId is also needed when requesting URLs for or deleting an uploaded file

    requestContent.Add(new StringContent(generateThumbnail.ToString().ToLower()), "generateThumbnail");
    // More information about thumbnail generation can be found in the README

    requestContent.Add(new StringContent(returnThumbnailUrl.ToString().ToLower()), "returnThumbnailUrl");

    if (thumbnailHeight.HasValue)
    {
        requestContent.Add(new StringContent(thumbnailHeight.ToString()), "thumbnailHeight");
    }

    if (thumbnailGeneratedCallbackUrl != null)
    {
        requestContent.Add(new StringContent(thumbnailGeneratedCallbackUrl), "thumbnailGeneratedCallbackUrl");
    }

    var responseMessage = await _httpClient.PostAsync("api/mediafiles", requestContent);
    var responseContent = await responseMessage.Content.ReadAsStringAsync();

    if (!responseMessage.IsSuccessStatusCode)
    {
        throw new Exception($"Upload failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"Upload response ({(int)responseMessage.StatusCode}): {responseContent}");
    // Upload response (200): {"assetId":"R2XxaflXtTMMfTTZnD6E5P53","mediafileId":"I7hUQIUkZKX7OfQIPRLz8fXb","thumbnailGenerated":true,"fileName":"image.png","links":[{"rel":"download","href":"https://media-a.antwerpen.be/download/15/I/I7hUQIUkZKX7OfQIPRLz8fXb/image.png"}]}

    // Warning: The resulting download URL is permanent URL which is accessible by anyone. It might be necessary to conceal this URL from end users, depending on your use case.
    // It's possible to configure your application to only generate temporary URLs, or to setup Access Control Lists for your files using the Digital Assets API (not available through Digital Assets Express).
    // Please contact the ACPaaS team for more information if needed.

    return JObject.Parse(responseContent);
}
```

### Get URLs

`GET /api/assets/{assetId}/mediafiles/{mediafileId}/urls?userid={userId}`

Response:

```json
{
  "mediafileDownloadUrl": "https://media-a.antwerpen.be/download/15/T/TpRSCyh6e3YujJsSlhZK0F2K/image.png",
  "mediaFileViewUrl": "https://media-a.antwerpen.be/media/15/T/TpRSCyh6e3YujJsSlhZK0F2K/image.png",
  "thumbnailUrl": "https://media-a.antwerpen.be/media/15/F/FXOLmfjL8QHdlJCuHG5tFQvJ/FXOLmfjL8QHdlJCuHG5tFQvJ.jpg"
}
```

Example implementation:

```csharp
public async Task<JObject> GetUrls(string assetId, string mediafileId, string userId)
{
    var responseMessage = await _httpClient.GetAsync($"api/assets/{assetId}/mediafiles/{mediafileId}/urls?userid={userId}");
    var responseContent = await responseMessage.Content.ReadAsStringAsync();

    if (!responseMessage.IsSuccessStatusCode)
    {
        throw new Exception($"GetUrls failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"GetUrls response ({(int)responseMessage.StatusCode}): {responseContent}");
    // GetUrls response (200): {"mediafileDownloadUrl":"https://media-a.antwerpen.be/download/15/T/TpRSCyh6e3YujJsSlhZK0F2K/image.png","mediaFileViewUrl":"https://media-a.antwerpen.be/media/15/T/TpRSCyh6e3YujJsSlhZK0F2K/image.png","thumbnailUrl":"https://media-a.antwerpen.be/media/15/F/FXOLmfjL8QHdlJCuHG5tFQvJ/FXOLmfjL8QHdlJCuHG5tFQvJ.jpg"}

    return JObject.Parse(responseContent);
}
```

Similar endpoints are available to get only the mediafile download URL or thumbnail URL.

Warning: The thumbnail URL might not be instantly available. Please refer to the "Thumbnail generation" section below for more information.

### Delete file

`DELETE /api/assets/{assetId}?userid={userId}`

Example implementation:

```csharp
public async Task Delete(string assetId, string userId)
{
    var responseMessage = await _httpClient.DeleteAsync($"api/assets/{assetId}?userid={userId}");

    if (!responseMessage.IsSuccessStatusCode)
    {
        var responseContent = await responseMessage.Content.ReadAsStringAsync();

        throw new Exception($"Delete failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"Delete response ({(int)responseMessage.StatusCode})");
    // Delete response (204)
}
```

## Example app

### Prerequisites

- .NET Core 3.0 SDK or Docker to build and run the application.
- API key of an application which has a contract with the Digital Assets Express API in ACC.

### Start example

```
dotnet run -- --api-key "<YOUR-API-KEY>"
```

Or using Docker:

```
docker build --tag digital-assets-express_example_dotnetcore .

docker run digital-assets-express_example_dotnetcore --api-key "<YOUR-API-KEY>"
```

You can change the default base URL and API key in [Config.cs](Config.cs).

### Example output

```
Starting Digital Assets Express example app
Using API key "<some API key>"
Upload response (200): {"assetId":"k2GSEskTlKj9SxSAgFxlaOmg","mediafileId":"T2CIrSQPUHOiLDDGRSuJCSNM","thumbnailGenerated":true,"fileName":"image.png","links":[{"rel":"download","href":"https://media-a.antwerpen.be/download/15/T/T2CIrSQPUHOiLDDGRSuJCSNM/image.png"}]}
GetUrl response (200): {"mediafileDownloadUrl":"https://media-a.antwerpen.be/download/15/T/T2CIrSQPUHOiLDDGRSuJCSNM/image.png"}
GetUrls response (200): {"mediafileDownloadUrl":"https://media-a.antwerpen.be/download/15/T/T2CIrSQPUHOiLDDGRSuJCSNM/image.png","mediaFileViewUrl":"https://media-a.antwerpen.be/media/15/T/T2CIrSQPUHOiLDDGRSuJCSNM/image.png","thumbnailUrl":"https://media-a.antwerpen.be/media/15/T/TXRmhQF8RZFTT2QMbeFcllLl/TXRmhQF8RZFTT2QMbeFcllLl.jpg"}
GetThumbnailUrl response (200): {"thumbnailUrl":"https://media-a.antwerpen.be/media/15/T/TXRmhQF8RZFTT2QMbeFcllLl/TXRmhQF8RZFTT2QMbeFcllLl.jpg"}
Delete response (204)
GetQuota response (200): {"appQuotaMb":"0","appDiskspaceUsedMb":"4843","quotaAvailableMb":"-4843"}
```

## Thumbnail generation

Thumbnail generation is an async process. This means the thumbnail URL might not be instantly available. Some considerations when using `generateThumbnail` when uploading files (`POST /api/mediafiles`):

- Be careful when using `returnThumbnailUrl`. The request will wait to complete until the thumbnail has been generated which might slow down your application. When the async thumbnail generation is temporarily delayed, this might even result in a timeout.
- An alternative is using `thumbnailGeneratedCallbackUrl` where your application provides an API endpoint which is called when the thumbnail has been generated. However, this endpoint will not be called when the async thumbnail generation is temporarily delayed by about one minute. This is a known limitation of the Digital Assets Express API.
- So your best option might be to poll the API to get the thumbnail URL (`GET /api/assets/{assetId}/thumbnail/url`).

An example for `thumbnailGeneratedCallbackUrl` is not provided in this application. You should pass the URL to a POST endpoint which accepts the following JSON payload:

```json
{
  "assetId": "k2GSEskTlKj9SxSAgFxlaOmg",
  "thumbnailUrl": "https://media-a.antwerpen.be/media/15/T/TXRmhQF8RZFTT2QMbeFcllLl/TXRmhQF8RZFTT2QMbeFcllLl.jpg"
}
```
