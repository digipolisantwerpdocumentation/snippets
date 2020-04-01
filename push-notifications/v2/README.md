# Notification Engine: Push Notification

The Notification Engine is a generic engine to send notifications.


### Links:

<!--ts-->
   * [Send notification](#send-notifications)
   * [General info](https://acpaas.digipolis.be/nl/product/notification-engine/v2.0.0/api-notification-engine-push-notif-v2/about#/Push32Notifications)
<!--te-->


## Send notification

**API documentation:** [Swagger](https://acpaas.digipolis.be/nl/product/notification-engine/v2.0.0/api-notification-engine-push-notif-v2/about#/Push32Notifications)

```javascript
const request = require('request-promise-native');
const correlation = require('astad-dgp-correlation');
const uuid = require('uuid');

const config = {
        host: 'https://api-gw-o.antwerpen.be', // Dev endpoint
        baseUrl: '/ballistix/push-notif-service/v2',
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
            	title: "Notificatie",
    			body: "{\"home\":\"go home\"}",
    			icon: "wave",
    			action: "/home",
    			applicationId: "app-id-testapp",
    			recipients: [
		        	{
            			userId: "aabc54f6d2b2afa8a7dc5d8b4568"
        			},
    			],
    			sendAt: "2019-09-05T09:22:00+02:00"
    		},
          json: true,
          resolveWithFullResponse: true,
          url: `${config.host}${config.baseUrl}/notifications`,
        };
        const response = await request.post(options);

    } catch (error) {
        console.log(`Post error ${error.message}`);
    }
}
send();

```
