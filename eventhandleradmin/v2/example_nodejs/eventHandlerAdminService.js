const axios = require("axios");

class EventHandlerAdminService {
    constructor(config) {
        this.axiosClient = axios.create({
            baseURL: config.baseAddress,
            headers: { "ApiKey": config.apiKey }
        });

        this.namespace = config.namespace;
        this.namespaceOwnerKey = config.namespaceOwnerKey;
        this.subscriptionOwnerKey = config.subscriptionOwnerKey;
    }

    async createTopic(name) {
        const response = await this.axiosClient.post(`/namespaces/${this.namespace}/topics`,
            { name },
            { headers: { "owner-key": this.namespaceOwnerKey } }
        );

        console.log(`CreateTopic response (${response.status}): ${JSON.stringify(response.data)}`);
        // CreateTopic response (201): {"name":"my-topic","namespace":"code-snippets-example-namespace"}

        return response.data;
    }

    async deleteTopic(name) {
        const response = await this.axiosClient.delete(`/namespaces/${this.namespace}/topics/${name}`,
            { headers: { "owner-key": this.namespaceOwnerKey } }
        );

        console.log(`DeleteTopic response (${response.status})`);
        // DeleteTopic response (200)
    }

    async createSubscription(topic, name, url) {
        const response = await this.axiosClient.post(`namespaces/${this.namespace}/subscriptions`,
            {
                name,
                topic,
                config: {
                    push: {
                        url
                    }
                }
            },
            { headers: { "owner-key": this.subscriptionOwnerKey } }
        );

        console.log(`CreateSubscription response (${response.status}): ${JSON.stringify(response.data)}`);
        // CreateSubscription response (201): {"name":"my-subscription","namespace":"code-snippets-example-namespace","owner":"mySubscriptionOwnerKey","topic":"my-topic","config":{"maxConcurrentDeliveries":1,"retries":{"firstLevelRetries":{"enabled":true,"retries":3,"onFailure":"error"},"secondLevelRetries":{"enabled":false,"retries":10,"ttl":600,"onFailure":"error"}},"push":{"pushType":"http","httpVerb":"POST","authentication":{"type":"none","kafka":{},"basic":{},"apikey":{},"oauth":{}},"url":"http://localhost/some-subscription-endpoint"},"restartAfterStop":{"enabled":false,"delayInMinutes":30}},"updated":"2020-03-23T18:39:19.527Z","created":"2020-03-23T18:39:19.528Z","status":"active"}

        return response.data;
    }

    async deleteSubscription(name) {
        const response = await this.axiosClient.delete(`/namespaces/${this.namespace}/subscriptions/${name}`,
            { headers: { "owner-key": this.subscriptionOwnerKey } }
        );

        console.log(`DeleteSubscription response (${response.status})`);
        // DeleteSubscription response (200)
    }

    async publish(topic, content) {
        const response = await this.axiosClient.post(`namespaces/${this.namespace}/topics/${topic}/publish`,
            content,
            { headers: { "owner-key": this.namespaceOwnerKey } }
        );

        console.log(`Publish response (${response.status})`);
        // Publish response (204)
    }
}

module.exports = EventHandlerAdminService;
