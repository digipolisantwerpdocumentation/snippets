# Digital Assets Express API example app (Node.js)

## Prerequisites

- Node.js (tested with 12.16.1) or Docker to run the application.
- API key of an application which has a contract with the Digital Assets Express API in ACC.

## Start example

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

## Example output

```
Starting Digital Assets Express API example app
Using API key "97fc3c32-d76d-431a-a5a2-0c9732a26c6e"
Upload response (200): {"assetId":"ygGahxsdQQNIRdhRRXvawXw9","mediafileId":"V2mKYbfTIgIRecHCslDq9pAj","thumbnailGenerated":true,"fileName":"image.png","links":[{"rel":"download","href":"https://media-a.antwerpen.be/download/15/V/V2mKYbfTIgIRecHCslDq9pAj/image.png"}]}
GetUrl response (200): {"mediafileDownloadUrl":"https://media-a.antwerpen.be/download/15/V/V2mKYbfTIgIRecHCslDq9pAj/image.png"}
GetUrls response (200): {"mediafileDownloadUrl":"https://media-a.antwerpen.be/download/15/V/V2mKYbfTIgIRecHCslDq9pAj/image.png","mediaFileViewUrl":"https://media-a.antwerpen.be/media/15/V/V2mKYbfTIgIRecHCslDq9pAj/image.png","thumbnailUrl":"https://media-a.antwerpen.be/media/15/u/uWJeOWYoHGPDaQTWFkNatm66/uWJeOWYoHGPDaQTWFkNatm66.jpg"}
GetThumbnailUrl response (200): {"thumbnailUrl":"https://media-a.antwerpen.be/media/15/u/uWJeOWYoHGPDaQTWFkNatm66/uWJeOWYoHGPDaQTWFkNatm66.jpg"}
Delete response (204)
GetQuota response (200): {"appQuotaMb":"0","appDiskspaceUsedMb":"4843","quotaAvailableMb":"-4843"}
```
