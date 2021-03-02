const WcmService = require("./wcmService");
const config = require("./config.json");

(async () => {
    try {
        console.log("Starting WCM API example app");

        const service = new WcmService({
            apiKey: config.apiKey,
            baseAddress: config.baseAddress,
            tenant: config.tenant,
        });

        const content = {
            meta: {
                activeLanguages: ["NL"],
                contentType: "5b921066534a07e22c43aece", // content type ID
                publishDate: "2020-10-20T11:00:00.000Z",
                slug: {
                    multiLanguage: true,
                    NL: "test-code-snippet",
                },
                label: "TestCodeSnippet",
                status: "PUBLISHED",
            },
            fields: {
                test: "Some test",
            },
        };

        const result = await service.createContentItem(content);
        await service.getContentItem(result.uuid);
        await service.deleteContentItem(result.uuid);
    } catch (ex) {
        console.log(`Something went wrong: ${ex.message}`);
    }
})();
