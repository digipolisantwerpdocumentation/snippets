# Digital Assets Express examples

The Digital Assets Express API provides a simplified API to access the Digital Assets engine.

## Examples

- [.NET Core examples](digital-assets-express_example_dotnetcore)
- [Node.js examples](digital-assets-express_example_nodejs)

## Links

- [Digital Assets engine on ACPaaS Portal](https://acpaas.digipolis.be/nl/product/digital-assets-engine)
- Digital Assets Express
  - [API Store ACC](https://api-store-a.antwerpen.be/#/org/acpaas/api/digitalassetsexpress/v1/documentation)
- Digital Assets
  - [API Store ACC](https://api-store-a.antwerpen.be/#/org/inuits/api/assets/v1/documentation)
  - [ACPaaS Wiki page](https://wiki.antwerpen.be/ACPAAS/index.php/Digital_Asset_express_engine)

## Note on thumbnail generation

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
