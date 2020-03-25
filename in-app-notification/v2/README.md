# In app notification


### Links:

<!--ts-->
   * [Get Notifications](#get-notifications)
   * [Get Messages](#get-messages)
   * [General info](https://acpaas.digipolis.be/nl/product/notification-engine/v2.0.0/api-notification-engine-in-app-v2/about)
<!--te-->


## Get Notifications

**API documentation:** [Swagger](https://api-store-o.antwerpen.be/#/org/ballistix/api/in-app-notification/v2/documentation)

```javascript
const request = require('request-promise-native');
const correlation = require('astad-dgp-correlation');
const uuid = require('uuid');

const config = {
        host: 'https://api-gw-o.antwerpen.be', // Dev endpoint
        baseUrl: 'ballistix/in-app-notification/v2',
        apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the api-store https://api-store-o.antwerpen.be/
}

async function getUserNotifications() {
    try {
        const dgpCorrelation = correlation.createDgpCorrelation(uuid.v4(), 'Your-service');
        const options = {
            headers: {
                'Dgp-Correlation': dgpCorrelation.asBase64,
                apikey: config.apiKey,
            },
            json: true,
            resolveWithFullResponse: true,
            url: `${config.host}${config.baseUrl}/inboxes/${userId}`,
        };
        const response = await request.get(options);

    } catch (error) {
        console.log(`error ${error.message}`);
    }
}
getUserNotifications();

```
## Get Messages

**API documentation:** [Swagger](https://api-store-o.antwerpen.be/#/org/ballistix/api/in-app-notification/v2/documentation)

```javascript
const request = require('request-promise-native');
const correlation = require('astad-dgp-correlation');
const uuid = require('uuid');

const config = {
        host: 'https://api-gw-o.antwerpen.be', // Dev endpoint
        baseUrl: 'ballistix/in-app-notification/v2',
        apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the api-store https://api-store-o.antwerpen.be/
}

async function getUserMessages() {
    try {
        const dgpCorrelation = correlation.createDgpCorrelation(uuid.v4(), 'Your-service');
        const options = {
            headers: {
                'Dgp-Correlation': dgpCorrelation.asBase64,
                apikey: config.apiKey,
            },
            json: true,
            resolveWithFullResponse: true,
            url: `${config.host}${config.baseUrl}/inboxes/${userId}/messages`,
        };
        const response = await request.get(options);

    } catch (error) {
        console.log(`error ${error.message}`);
    }
}
getUserMessages();

```
