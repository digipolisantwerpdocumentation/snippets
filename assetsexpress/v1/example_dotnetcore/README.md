# Digital Assets Express example app (.NET Core)

## Prerequisites

- .NET Core 3.0 SDK or Docker to build and run the application.
- API key of an application which has a contract with the Digital Assets Express API in ACC.

## Start example

```
dotnet run -- --api-key "<YOUR-API-KEY>"
```

Or using Docker:

```
docker build --tag digital-assets-express_example_dotnetcore .

docker run digital-assets-express_example_dotnetcore --api-key "<YOUR-API-KEY>"
```

You can change the default base URL and API key in [Config.cs](Config.cs).

## Example output

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
