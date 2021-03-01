# In app notification


### Links:

<!--ts-->
* [API documentation](#api-documentation)
* [Example app .net](#example-app-net)
* [Code snippets NodeJs](#code-snippets-nodejs)
    - [Get Inbox overview](#get-inbox-overview)
    - [Get Messages](#get-messages)
* [General info](https://acpaas.digipolis.be/nl/product/notification-engine/v2.0.0/api-notification-engine-in-app-v2/about)
<!--te-->


## API documentation
**in-app notification:** [Swagger](https://api-store-a.antwerpen.be/#/org/acpaas/api/in-app-notification/v2/documentation)


## Example app .net

.net 5.0 code snippets for in-app notifications are available in the sample app: [InAppNotificationService.cs (.NET 5.0)](example_dotnet5/InAppNotificationService.cs)

### Setup

First configure the base URL and API key in Config.cs:

**.NET 5.0:**

```csharp
public static class Config
    {
        public static string ApiKey = "<YOUR-API-KEY>";

        public static string BaseAddress = "https://api-gw-a.antwerpen.be/acpaas/in-app-notification/v2/";
    }
```


## Code snippets NodeJs
## Get Inbox overview

```javascript
const request = require('request-promise-native');
const correlation = require('astad-dgp-correlation');
const uuid = require('uuid');

const config = {
        host: 'https://api-gw-a.antwerpen.be', // ACC endpoint
        baseUrl: 'acpaas/in-app-notification/v2',
        apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the api-store https://api-store-a.antwerpen.be/
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

```javascript
const request = require('request-promise-native');
const correlation = require('astad-dgp-correlation');
const uuid = require('uuid');

const config = {
        host: 'https://api-gw-a.antwerpen.be', // ACC endpoint
        baseUrl: 'acpaas/in-app-notification/v2',
        apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the api-store https://api-store-a.antwerpen.be/
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
