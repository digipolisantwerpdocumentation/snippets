const rp = require("request-promise-native");

class DigitalAssetsExpressService {
    constructor(config) {
        this.requestClient = rp.defaults({
            baseUrl: config.baseAddress,
            headers: { "ApiKey": config.apiKey },
            resolveWithFullResponse: true
        });
    }

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

    async getUrl(assetId, mediafileId, userId) {
        const response = await this.requestClient.get(`api/assets/${assetId}/mediafiles/${mediafileId}/url?userid=${userId}`);

        console.log(`GetUrl response (${response.statusCode}): ${response.body}`);
        // GetUrl response (200): {"mediafileDownloadUrl":"https://media-a.antwerpen.be/download/15/V/V2mKYbfTIgIRecHCslDq9pAj/image.png"}

        return JSON.parse(response.body);
    }

    async getUrls(assetId, mediafileId, userId) {
        const response = await this.requestClient.get(`api/assets/${assetId}/mediafiles/${mediafileId}/urls?userid=${userId}`);

        console.log(`GetUrls response (${response.statusCode}): ${response.body}`);
        // GetUrls response (200): {"mediafileDownloadUrl":"https://media-a.antwerpen.be/download/15/V/V2mKYbfTIgIRecHCslDq9pAj/image.png","mediaFileViewUrl":"https://media-a.antwerpen.be/media/15/V/V2mKYbfTIgIRecHCslDq9pAj/image.png","thumbnailUrl":"https://media-a.antwerpen.be/media/15/u/uWJeOWYoHGPDaQTWFkNatm66/uWJeOWYoHGPDaQTWFkNatm66.jpg"}

        return JSON.parse(response.body);
    }

    async getThumbnailUrl(assetId, userId) {
        const response = await this.requestClient.get(`api/assets/${assetId}/thumbnail/url?userid=${userId}`);

        console.log(`GetThumbnailUrl response (${response.statusCode}): ${response.body}`);
        // GetThumbnailUrl response (200): {"thumbnailUrl":"https://media-a.antwerpen.be/media/15/u/uWJeOWYoHGPDaQTWFkNatm66/uWJeOWYoHGPDaQTWFkNatm66.jpg"}

        return JSON.parse(response.body);
    }

    async delete(assetId, userId) {
        const response = await this.requestClient.delete(`api/assets/${assetId}?userid=${userId}`);

        console.log(`Delete response (${response.statusCode})`);
        // Delete response (204)
    }

    async getQuota() {
        const response = await this.requestClient.get(`apps/quota`);

        console.log(`GetQuota response (${response.statusCode}): ${response.body}`);
        // GetQuota response (200): {"appQuotaMb":"0","appDiskspaceUsedMb":"4843","quotaAvailableMb":"-4843"}

        return JSON.parse(response.body);
    }
}

module.exports = DigitalAssetsExpressService;
