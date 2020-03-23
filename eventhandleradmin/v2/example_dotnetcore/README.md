# Event Handler Admin example app (.NET Core)

## Prerequisites

- .NET Core 3.0 SDK or Docker to build and run the application.
- API key of an application which has a contract with the Event Handler Admin API in ACC.
- Event Handler owner key of an existing namespace.

## Start example

```
dotnet run -- --api-key "<YOUR-API-KEY>" --namespace-owner-key "myNamespaceOwnerKey"
```

Or using Docker:

```
docker build --tag eventhandler-admin_example_dotnetcore .

docker run eventhandler-admin_example_dotnetcore --api-key "<YOUR-API-KEY>" --namespace-owner-key "myNamespaceOwnerKey"
```

You can change the default configuration in [Config.cs](Config.cs).

## Example output

```
Starting Event Handler Admin example app
Using API key "<YOUR-API-KEY>"
Using namespace owner key "myNamespaceOwnerKey"
Using subscription owner key "mySubscriptionOwnerKey"
Using namespace "code-snippets-example-namespace"
Using topic "my-topic"
Create topic response (201): {"name":"my-topic","namespace":"code-snippets-example-namespace"}
Create subscription response (201): {"name":"my-subscription","namespace":"code-snippets-example-namespace","owner":"mySubscriptionOwnerKey","topic":"my-topic","config":{"maxConcurrentDeliveries":1,"retries":{"firstLevelRetries":{"enabled":true,"retries":3,"onFailure":"error"},"secondLevelRetries":{"enabled":false,"retries":10,"ttl":600,"onFailure":"error"}},"push":{"pushType":"http","httpVerb":"POST","authentication":{"type":"none","kafka":{},"basic":{},"apikey":{},"oauth":{}},"url":"http://localhost/some-subscription-endpoint"},"restartAfterStop":{"enabled":false,"delayInMinutes":30}},"updated":"2020-03-23T15:32:34.667Z","created":"2020-03-23T15:32:34.668Z","status":"active"}
Publish response (204)
Delete subscription response (200)
Delete topic response (200)
```
