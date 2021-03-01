# Notification Engine: Push Notification

The Notification Engine is a generic engine to send notifications.


### Links:

<!--ts-->
* [API documentation](#api-documentation)
* [Example app .net](#example-app-net)
* [Code snippets NodeJs](#code-snippets-nodejs)
    - [Send notification](#send-notifications)
* [General info](https://acpaas.digipolis.be/nl/product/notification-engine/v2.0.0/api-notification-engine-push-notif-v2/about#/Push32Notifications)
<!--te-->

## API documentation
**push notification:** [Swagger](https://api-store-a.antwerpen.be/#/org/acpaas/api/push-notification-service/v2/documentation)


## Example app .net

.net 5.0 code snippets for sending a push notification are available in the sample app: [PushNotificationService.cs (.NET 5.0)](example_dotnet5/PushNotificationService.cs)

### Setup

First configure the base URL and API key in Config.cs:

**.NET 5.0:**

```csharp
public static class Config
    {
        public static string ApiKey = "<YOUR-API-KEY>";

        public static string BaseAddress = "https://api-gw-a.antwerpen.be/acpaas/push-notif-service/v2/";
    }
```


## Code snippets NodeJs
## Send notification

```javascript
const request = require('request-promise-native');
const correlation = require('astad-dgp-correlation');
const uuid = require('uuid');

const config = {
        host: 'https://api-gw-a.antwerpen.be', // ACC endpoint
        baseUrl: '/acpaas/push-notif-service/v2',
        apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the api-store https://api-store-a.antwerpen.be/
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
    			]
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
