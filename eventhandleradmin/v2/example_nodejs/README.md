# Event Handler Admin example app (Node.js)

## Prerequisites

- Node.js (tested with 12.16.1) or Docker to run the application.
- API key of an application which has a contract with the Event Handler Admin API in ACC.
- Event Handler owner key of an existing namespace.

## Start example

```
npm install

node index.js --apiKey "<YOUR-API-KEY>" --namespaceOwnerKey "myNamespaceOwnerKey"
```

Or using Docker:

```
docker build --tag eventhandler-admin_example_nodejs .

docker run eventhandler-admin_example_nodejs --apiKey "<YOUR-API-KEY>" --namespaceOwnerKey "myNamespaceOwnerKey"
```

You can change the default configuration in [config.json](config.json).

## Example output

```
Starting Event Handler Admin example app
Using API key "<YOUR-API-KEY>"
Using namespace owner key "myNamespaceOwnerKey"
Using subscription owner key "mySubscriptionOwnerKey"
Using namespace "code-snippets-example-namespace"
Using topic "my-topic"
Using subscription "my-subscription"
myNamespaceOwnerKey
CreateTopic response (201): {"name":"my-topic","namespace":"code-snippets-example-namespace"}
CreateSubscription response (201): {"name":"my-subscription","namespace":"code-snippets-example-namespace","owner":"mySubscriptionOwnerKey","topic":"my-topic","config":{"maxConcurrentDeliveries":1,"retries":{"firstLevelRetries":{"enabled":true,"retries":3,"onFailure":"error"},"secondLevelRetries":{"enabled":false,"retries":10,"ttl":600,"onFailure":"error"}},"push":{"pushType":"http","httpVerb":"POST","authentication":{"type":"none","kafka":{},"basic":{},"apikey":{},"oauth":{}},"url":"http://localhost/some-subscription-endpoint"},"restartAfterStop":{"enabled":false,"delayInMinutes":30}},"updated":"2020-03-23T18:39:19.527Z","created":"2020-03-23T18:39:19.528Z","status":"active"}
Publish response (204): ""
DeleteSubscription response (200)
DeleteTopic response (200)
```
