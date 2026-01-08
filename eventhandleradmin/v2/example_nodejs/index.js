const argv = require('yargs').argv;

const EventHandlerAdminService = require("./eventHandlerAdminService");
const config = require("./config.json");

(async () => {
    try {
        console.log("Starting Event Handler Admin example app");

        const apiKey = argv.apiKey || config.apiKey;
        const namespaceOwnerKey = argv.namespaceOwnerKey || config.namespaceOwnerKey;
        const subscriptionOwnerKey = argv.subscriptionOwnerKey || config.subscriptionOwnerKey;
        const namespace = argv.namespace || config.namespace;
        const topic = argv.topic || config.topic;
        const subscription = argv.subscription || config.subscription;

        console.log(`Using API key from ${argv.apiKey ? "command line argument" : "configuration"} (value redacted)`);
        console.log(`Using namespace owner key from ${argv.namespaceOwnerKey ? "command line argument" : "configuration"} (value redacted)`);
        console.log(`Using subscription owner key from ${argv.subscriptionOwnerKey ? "command line argument" : "configuration"} (value redacted)`);
        console.log(`Using namespace "${namespace}"`);
        console.log(`Using topic "${topic}"`);
        console.log(`Using subscription "${subscription}"`);

        const service = new EventHandlerAdminService({
            apiKey,
            baseAddress: config.baseAddress,
            namespace,
            namespaceOwnerKey,
            subscriptionOwnerKey
        });

        await service.createTopic(topic);
        await service.createSubscription(topic, subscription, "http://localhost/some-subscription-endpoint");
        await service.publish(topic, { someProperty: "some event message" })
        await service.deleteSubscription(subscription);
        await service.deleteTopic(topic);
    } catch (ex) {
        console.log(`Something went wrong: ${ex.message}`);

        if (ex.response.data) {
            // Response data from Axios
            console.log(ex.response.data);
        }
    }
})();
