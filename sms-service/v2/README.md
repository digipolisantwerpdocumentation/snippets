# Notification Engine: SMS-Service


### Links:

<!--ts-->
   * [Send sms](#send-sms)
   * [General info](https://acpaas.digipolis.be/nl/product/notification-engine/v2.0.0/api-notification-preference-v2/about)
<!--te-->


## Send sms

**API documentation:** [Swagger](https://acpaas.digipolis.be/nl/product/notification-engine/v2.0.0/api-notification-engine-sms-service-v2/about)

```javascript
const request = require('request-promise-native');
const correlation = require('astad-dgp-correlation');
const uuid = require('uuid');

const config = {
        host: 'https://api-gw-o.antwerpen.be', // Dev endpoint
        baseUrl: '/ballistix/sms-service/v2',
        apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the api-store https://api-store-o.antwerpen.be/
}

async function send() {
    try {
        const dgpCorrelation = correlation.createDgpCorrelation(uuid.v4(), 'Your-service');
        const options = {
            headers: {
                'Dgp-Correlation': dgpCorrelation.asBase64,
                apikey: config.apiKey,
            },
            body: {
    			recipients: [
        			{
            			number: "04xxxxxxxxx"
        			}
    			],
    			text: "Hello world, dit is een sms bericht",
    			sendAt: "2019-09-05T09:20:00+02:00",
    			priority: true
    		},
            json: true,
            resolveWithFullResponse: true,
            url: `${config.host}${config.baseUrl}/sms`,
        };
        const response = await request.post(options);
    } catch (error) {
        console.log(`error ${error.message}`);
    }
}
send();

```
