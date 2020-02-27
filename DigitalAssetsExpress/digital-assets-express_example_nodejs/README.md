# Digital Assets Express examples (Node.js)

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

These snippets and other examples are available in [digitalAssetsExpressService.js](digitalAssetsExpressService.js).

First configure the base URL and API key:

```js
class DigitalAssetsExpressService {
    constructor(config) {
        this.requestClient = rp.defaults({
            baseUrl: config.baseAddress,
            headers: { "ApiKey": config.apiKey },
            resolveWithFullResponse: true
        });
    }
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

```js
async upload(file, fileName, userId, generateThumbnail = false, returnThumbnailUrl = false, thumbnailGeneratedCallbackUrl = null, thumbnailHeight = null) {
    const formData = {
        userId,
        // The userId property can be used to specify the user who uploaded the file
        // The same userId is also needed when requesting URLs for or deleting an uploaded file

        file: {
            value: file,
            options: {
                filename: fileName,
                contentType: "application/octet-stream"
            }
        },
        generateThumbnail: generateThumbnail.toString(),
        // More information about thumbnail generation can be found in the README

        returnThumbnailUrl: returnThumbnailUrl.toString()
    };

    if (thumbnailGeneratedCallbackUrl != null) {
        formData.thumbnailGeneratedCallbackUrl = thumbnailGeneratedCallbackUrl;
    }

    if (thumbnailHeight != null) {
        formData.thumbnailHeight = thumbnailHeight;
    }

    const response = await this.requestClient.post("api/mediafiles", { formData });

    console.log(`Upload response (${response.statusCode}): ${response.body}`);
    // Upload response (200): {"assetId":"ygGahxsdQQNIRdhRRXvawXw9","mediafileId":"V2mKYbfTIgIRecHCslDq9pAj","thumbnailGenerated":true,"fileName":"image.png","links":[{"rel":"download","href":"https://media-a.antwerpen.be/download/15/V/V2mKYbfTIgIRecHCslDq9pAj/image.png"}]}

    // Warning: The resulting download URL is permanent URL which is accessible by anyone. It might be necessary to conceal this URL from end users, depending on your use case.
    // It's possible to configure your application to only generate temporary URLs, or to setup Access Control Lists for your files using the Digital Assets API (not available through Digital Assets Express).
    // Please contact the ACPaaS team for more information if needed.

    return JSON.parse(response.body);
}
```

The `file` passed to `upload()` should be a buffer or a file stream. Refer to [index.js](index.js) for an example using `fs.createReadStream()`.

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

```js
async getUrls(assetId, mediafileId, userId) {
    const response = await this.requestClient.get(`api/assets/${assetId}/mediafiles/${mediafileId}/urls?userid=${userId}`);

    console.log(`GetUrls response (${response.statusCode}): ${response.body}`);
    // GetUrls response (200): {"mediafileDownloadUrl":"https://media-a.antwerpen.be/download/15/V/V2mKYbfTIgIRecHCslDq9pAj/image.png","mediaFileViewUrl":"https://media-a.antwerpen.be/media/15/V/V2mKYbfTIgIRecHCslDq9pAj/image.png","thumbnailUrl":"https://media-a.antwerpen.be/media/15/u/uWJeOWYoHGPDaQTWFkNatm66/uWJeOWYoHGPDaQTWFkNatm66.jpg"}

    return JSON.parse(response.body);
}
```

Similar endpoints are available to get only the mediafile download URL or thumbnail URL.

Warning: The thumbnail URL might not be instantly available. Please refer to the "Thumbnail generation" section below for more information.

### Delete file

`DELETE /api/assets/{assetId}?userid={userId}`

Example implementation:

```js
async delete(assetId, userId) {
    const response = await this.requestClient.delete(`api/assets/${assetId}?userid=${userId}`);

    console.log(`Delete response (${response.statusCode})`);
    // Delete response (204)
}
```

## Example app

### Prerequisites

- Node.js (tested with 12.16.1) or Docker to run the application.
- API key of an application which has a contract with the Digital Assets Express API in ACC.

### Start example

```
npm install

node index.js --apiKey "<YOUR-API-KEY>"
```

Or using Docker:

```
docker build --tag digital-assets-express_example_nodejs .

docker run digital-assets-express_example_nodejs --apiKey "<YOUR-API-KEY>"
```

You can change the default base URL and API key in [config.json](config.json).

### Example output

```
Starting Digital Assets Express example app
Using API key "97fc3c32-d76d-431a-a5a2-0c9732a26c6e"
Upload response (200): {"assetId":"ygGahxsdQQNIRdhRRXvawXw9","mediafileId":"V2mKYbfTIgIRecHCslDq9pAj","thumbnailGenerated":true,"fileName":"image.png","links":[{"rel":"download","href":"https://media-a.antwerpen.be/download/15/V/V2mKYbfTIgIRecHCslDq9pAj/image.png"}]}
GetUrl response (200): {"mediafileDownloadUrl":"https://media-a.antwerpen.be/download/15/V/V2mKYbfTIgIRecHCslDq9pAj/image.png"}
GetUrls response (200): {"mediafileDownloadUrl":"https://media-a.antwerpen.be/download/15/V/V2mKYbfTIgIRecHCslDq9pAj/image.png","mediaFileViewUrl":"https://media-a.antwerpen.be/media/15/V/V2mKYbfTIgIRecHCslDq9pAj/image.png","thumbnailUrl":"https://media-a.antwerpen.be/media/15/u/uWJeOWYoHGPDaQTWFkNatm66/uWJeOWYoHGPDaQTWFkNatm66.jpg"}
GetThumbnailUrl response (200): {"thumbnailUrl":"https://media-a.antwerpen.be/media/15/u/uWJeOWYoHGPDaQTWFkNatm66/uWJeOWYoHGPDaQTWFkNatm66.jpg"}
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
