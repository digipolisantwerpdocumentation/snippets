# Notification Engine: preferences

The Notification Engine is a generic engine to send notification.


### Links:

<!--ts-->
   * [Get contextpreferences](#get-contextpreferences)
   * [Create contextpreference](#create-contextpreference)
   * [General info](https://acpaas.digipolis.be/nl/product/notification-engine/v2.0.0/api-notification-preference-v2/about)
<!--te-->


## Get contextpreferences

**API documentation:** [Swagger](https://api-store-o.antwerpen.be/#/org/ballistix/api/notificiation-preference/v2/documentation)

```javascript
const request = require('request-promise-native');
const correlation = require('astad-dgp-correlation');
const uuid = require('uuid');

const config = {
        host: 'https://api-gw-a.antwerpen.be', // Dev endpoint
        baseUrl: '/ballistix/notif-preference/v2',
        apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the api-store https://api-store-o.antwerpen.be/
}

async function get() {
    try {
        const dgpCorrelation = correlation.createDgpCorrelation(uuid.v4(), 'Your-service');
        const options = {
            headers: {
                'Dgp-Correlation': dgpCorrelation.asBase64,
                apikey: config.apiKey,
            },
            json: true,
            resolveWithFullResponse: true,
            url: `${config.host}${config.baseUrl}/contextpreferences`,
        };
        const response = await request.get(options);

    } catch (error) {
        console.log(`error ${error.message}`);
    }
}
get();

```
## Create contextpreference

**API documentation:** [Swagger](https://api-store-o.antwerpen.be/#/org/ballistix/api/notificiation-preference/v2/documentation)

```javascript
const request = require('request-promise-native');
const correlation = require('astad-dgp-correlation');
const uuid = require('uuid');

const config = {
        host: 'https://api-gw-o.antwerpen.be', // Dev endpoint
        baseUrl: '/ballistix/notif-preference/v2',
        apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the api-store https://api-store-o.antwerpen.be/
}

async function create() {
    try {
        const dgpCorrelation = correlation.createDgpCorrelation(uuid.v4(), 'Your-service');
        const options = {
            headers: {
                'Dgp-Correlation': dgpCorrelation.asBase64,
                apikey: config.apiKey,
            },
            body: {
                contextName: "contextName",
                channel: "channel",
                language: "nl",
                userId: "userId"
            },
            json: true,
            resolveWithFullResponse: true,
            url: `${config.host}${config.baseUrl}/contextpreferences`,
        };
        const response = await request.post(options);

    } catch (error) {
        console.log(`Post error ${error.message}`);
    }
}
create();

```
