# Notification Engine

The Notification Engine is a generic engine to send notifications.


### Links:

<!--ts-->
* [API documentation](#api-documentation)
* [Available channels](#available-channels)
* [Setup](#setup)
* [Admin panel](#notification-engine-admin)
* [Example app .net](#example-app-net)
* [Code snippets NodeJs](#code-snippets-nodejs)
    - [Message to topic](#send-a-message-to-a-topic)
    - [Create a topic](#create-a-topic)
* [General info](https://acpaas.digipolis.be/nl/product/notification-engine/v2.0.0/gettingStarted)
<!--te-->

## API documentation
**notification orchestrator:** [Swagger](https://api-store-o.antwerpen.be/#/org/ballistix/api/notification-orchestrator/v2/documentation)
**notification preference admin:** [Swagger](https://api-store-o.antwerpen.be/#/org/ballistix/api/notification-preference-admin/v2/documentation)


## Available channels:
The Notification Engine has multiple channels of communication:

- sms
- e-mail
- push notifications
- in-app notifications


## Setup
Create a contract with the orchestrator & follow the security setup to gain access rights to the API

1. [Security setup](https://wiki.antwerpen.be/ACPAAS/index.php/Notification_Engine_v2_-_Technical_documentation#Security) *(internal access needed)*
2. [Orchestrator](https://api-store-o.antwerpen.be/#/org/ballistix/api/notification-orchestrator/v2/documentation)


## Notification Engine Admin
Here you can see the sent messages and preferences on your tenant. (You will first have to request permissions to gain access to this panel & send a message to make your tenant visible).

[Admin panel](https://notif-admin-o.antwerpen.be/) (dev)


## Example app .net

.net 5.0 code snippets for sending a notification and creating a topic are available in the sample app: [NotificationOrchestratorService.cs (.NET Core)](example_dotnet5/NotificationOrchestratorService.cs)

### Setup

First configure the base URL and API key in Config.cs:

**.NET 5.0:**

```csharp
public static class Config
    {
        public static string ApiKey = "<YOUR-API-KEY>";

        public static string BaseAddressPreferenceAdmin = "https://api-gw-o.antwerpen.be/ballistix/notif-pref-admin/v2";

        public static string BaseAddressOrchestrator = "https://api-gw-o.antwerpen.be/ballistix/notif-orchestrator/v2";
    }
```

## Code snippets NodeJs
### Send a message to a topic:

```javascript
const request = require('request-promise-native');
const moment = require('moment');
const correlation = require('astad-dgp-correlation');

const config = {
    host: 'https://api-gw-o.antwerpen.be', // Dev endpoint
    baseUrl: '/ballistix/notif-orchestrator/v2',
    apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the api-store https://api-store-o.antwerpen.be/
}

async function send(message, topic) {
    try {
        const dgpCorrelation = correlation.createDgpCorrelation(uuid.v4(), 'Your-service');
        const options = {
            body: {
                recipients: [
                    { topicName: topic },
                ],
                sendAt: moment().toISOString(),
                email: {
                    subject: `Status update for topic: ${topic}`,
                    from: 'your-sender@antwerpen.be',
                    html: `<b>${message}</b>`,
                    text: message,
                },
            },
            headers: {
                'Dgp-Correlation': dgpCorrelation.asBase64,
                apikey: config.apiKey,
            },
            json: true,
            resolveWithFullResponse: true,
            url: `${baseUrlOrchestrator}/notifications`,
        };
        const response = await request.post(options);

    } catch (error) {
        console.log(`Post error ${error.message}`);
    }

}
send('Hello world', 'topic1');
```
### Create a topic

```javascript
const request = require('request-promise-native');
const correlation = require('astad-dgp-correlation');

const config = {
    host: 'https://api-gw-o.antwerpen.be', // Dev endpoint
    baseUrl: '/ballistix/notif-pref-admin/v2',
    apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the api-store https://api-store-o.antwerpen.be/
}

async function createTopic(topic) {
    try {
        const dgpCorrelation = correlation.createDgpCorrelation(uuid.v4(), 'Your-service');
        const options = {
            body: {
                supportedLanguages: ['en'],
                defaultLanguage: 'en',
                name: topic,
                defaultChannel: 'email',
                supportedChannels: ['email'],
            },
            headers: {
                'Dgp-Correlation': dgpCorrelation.asBase64,
                apikey: config.apiKey,
            },
            json: true,
            resolveWithFullResponse: true,
            url: `${baseUrlAdmin}/topics`,
        };
        const response = await request.post(options);

    } catch (error) {
        console.log(`Post error ${error.message}`);
    }

}
createTopic('topic1');
```
