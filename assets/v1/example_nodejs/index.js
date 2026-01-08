const argv = require('yargs').argv;
const fs = require("fs");
const assert = require("assert");

const AssetsService = require("./assetsService");
const config = require("./config.json");

(async () => {
    try {
        console.log("Starting Assets API example app");

        const apiKey = argv.apiKey || config.apiKey;

        const redactedApiKey =
            typeof apiKey === "string" && apiKey.length > 4
                ? apiKey.slice(-4)
                : "<unknown>";
        console.log(`Using API key ending with "${redactedApiKey}" (redacted)`);

        const service = new AssetsService({
            apiKey,
            baseAddress: config.baseAddress
        });

        const userId = "my-user-id";
        const secondUserId = "second-user-id";
        const thirdUserId = "third-user-id";
        const fileName = "image.png";
        const file = fs.createReadStream(fileName);

        const createUploadTicketResult = assertSuccess(await service.createUploadTicket(userId));

        const ticketId = createUploadTicketResult.response.items.item.ticket_id;
        const assetId = createUploadTicketResult.response.items.item.asset_id;
        const mediafileId = createUploadTicketResult.response.items.item.mediafile_id;

        assertSuccess(await service.uploadFile(ticketId, file, fileName, true, 20));

        // Get URL
        assertSuccess(await service.getMediafileView(assetId, userId, "plain", mediafileId));

        // Get thumbnail URL (you should poll for the thumbnail URL as it's not instantly available)
        assertSuccess(await service.getMediafileView(assetId, userId, "still", mediafileId));

        // Get URL using another user ID (this will succeed because no ACL rules have been set on the mediafile)
        assertSuccess(await service.getMediafileView(assetId, secondUserId, "plain", mediafileId));

        // Set ACL rule so only specific users can access the mediafile
        assertSuccess(await service.setMediafileAcl(mediafileId, userId, secondUserId));

        // Get URL using another user ID (this will succeed because user has access through ACL rule)
        // Note that the URL is now temporary instead of permanent
        assertSuccess(await service.getMediafileView(assetId, secondUserId, "plain", mediafileId));

        // Get URL using yet another user ID (this will fail because user has no access through ACL rule)
        assertFailure(await service.getMediafileView(assetId, thirdUserId, "plain", mediafileId));

        assertSuccess(await service.delete(assetId, userId));

        assertSuccess(await service.getQuota());
    } catch (ex) {
        console.log(`Something went wrong: ${ex.message}`);
    }
})();

function assertSuccess(result) {
    assert.equal(getRequestResult(result), "success");

    return result;
}

function assertFailure(result) {
    assert.equal(getRequestResult(result), "error");

    return result;
}

function getRequestResult(result) {
    return result.response.header.request_result;
}
