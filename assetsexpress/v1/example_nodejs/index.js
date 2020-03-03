const argv = require('yargs').argv;
const fs = require("fs");

const DigitalAssetsExpressService = require("./digitalAssetsExpressService");
const config = require("./config.json");

(async () => {
    try {
        console.log("Starting Digital Assets Express example app");

        const apiKey = argv.apiKey || config.apiKey;

        console.log(`Using API key "${apiKey}"`);

        const service = new DigitalAssetsExpressService({
            apiKey,
            baseAddress: config.baseAddress
        });

        const userId = "my-user-id";
        const fileName = "image.png";
        const file = fs.createReadStream(fileName);

        const uploadResult = await service.upload(file, fileName, userId, true);

        await service.getUrl(uploadResult.assetId, uploadResult.mediafileId, userId);
        await service.getUrls(uploadResult.assetId, uploadResult.mediafileId, userId);
        await service.getThumbnailUrl(uploadResult.assetId, userId);

        await service.delete(uploadResult.assetId, userId);

        await service.getQuota();
    } catch (ex) {
        console.log(`Something went wrong: ${ex.message}`);
    }
})();
