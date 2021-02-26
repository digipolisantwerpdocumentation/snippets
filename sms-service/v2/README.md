# Notification Engine: SMS-Service


### Links:

<!--ts-->
* [Example app .net](#example-app-net)
* [Code snippets NodeJs](#code-snippets-nodejs)
    - [Send sms](#send-sms)
* [General info](https://acpaas.digipolis.be/nl/product/notification-engine/v2.0.0/api-notification-engine-sms-service-v2/about)
<!--te-->

## API documentation
**sms service:** [Swagger](https://api-store-a.antwerpen.be/#/org/acpaas/api/sms-service/v2/documentation)


## Example app .net

.net 5.0 code snippets for sending a sms are available in the sample app: [SmsService.cs (.NET 5.0)](example_dotnet5/SmsService.cs)

### Setup

First configure the base URL and API key in Config.cs:

**.NET 5.0:**

```csharp
public static class Config
    {
        public static string ApiKey = "<YOUR-API-KEY>";

        public static string BaseAddress = "https://api-gw-a.antwerpen.be/acpaas/sms-service/v2/";
    }
```


## Code snippets NodeJs
### Send sms

```javascript
const request = require('request-promise-native');
const correlation = require('astad-dgp-correlation');
const uuid = require('uuid');

const config = {
        host: 'https://api-gw-a.antwerpen.be', // Dev endpoint
        baseUrl: '/acpaas/sms-service/v2',
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
