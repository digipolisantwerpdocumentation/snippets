# Notification Engine

The Notification Engine is a generic engine to send notification.


### Table of contents:

<!--ts-->
   * [Available channels](#available-channels)
   * [Setup](#setup)
   * [Admin panel](#notification-engine-admin)
   * [Message to topic](#send-a-message-to-a-topic)
   * [Create to topic](#send-a-message-to-a-topic)
   * [General info](https://acpaas.digipolis.be/nl/product/notification-engine/v2.0.0/gettingStarted)
   * [User manual](https://wiki.antwerpen.be/ACPAAS/index.php/Event-Handler_User_Manuals) *(internal access needed)*
<!--te-->


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
Here you can see the send messages, preferences on your tenant. (You will first have to request permissions to gain access to this panel & send a message to make your tenant visible).

[Admin panel](https://notif-admin-o.antwerpen.be/) (dev)



## Send a message to a topic:

**API documentation:** [Swagger](https://api-store-o.antwerpen.be/#/org/ballistix/api/notification-orchestrator/v2/documentation)

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
## Create a topic

**API documentation:** [Swagger](https://api-store-o.antwerpen.be/#/org/ballistix/api/notification-preference-admin/v2/documentation)

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
