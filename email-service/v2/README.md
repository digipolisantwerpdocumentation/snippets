# Notification Engine: email-Service


### Links:

<!--ts-->
* [Example app .net](#example-app-net)
* [Code snippets NodeJs](#code-snippets-nodejs)
    - [Send email](#send-email)
* [General info](https://acpaas.digipolis.be/nl/product/notification-engine/v2.0.0/api-notification-engine-email-service-v2/about)
<!--te-->

## API documentation
**e-mail service:** [Swagger](https://api-store-a.antwerpen.be/#/org/acpaas/api/email-service/v2/documentation)


## Example app .net

.net 5.0 code snippets for sending an e-mail are available in the sample app: [EmailService.cs (.NET 5.0)](example_dotnet5/EmailService.cs)

### Setup

First configure the base URL and API key in Config.cs:

**.NET 5.0:**

```csharp
public static class Config
    {
        public static string ApiKey = "<YOUR-API-KEY>";

        public static string BaseAddress = "https://api-gw-a.antwerpen.be/acpaas/email-service/v2/";
    }
```


## Code snippets NodeJs
## Send email

```javascript
const request = require('request-promise-native');
const correlation = require('astad-dgp-correlation');
const uuid = require('uuid');

const config = {
        host: 'https://api-gw-a.antwerpen.be', // Dev endpoint
        baseUrl: '/acpaas/email-service/v2',
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
            	subject: "Test email",
            	from: "test@digipolis.be",
            	recipients: [
            	{
            		email: "test@digipolis.be"
            	}
            	],
            	sendAt: "2019-09-05T09:10:00+02:00",
   				priority: true,
    			attachments: [],
    			inlineImages: [],
    			html: "<html xmlns=\"http://www.w3.org/1999/xhtml\"><head></head><body><p>Dit is een html bericht</p></body></html>"
    		},
            json: true,
            resolveWithFullResponse: true,
            url: `${config.host}${config.baseUrl}/emails`,
        };
        const response = await request.post(options);
    } catch (error) {
        console.log(`error ${error.message}`);
    }
}
send();

```
