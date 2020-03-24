![eventhandler](./assets/eventhandler.png)

# Event Handler Admin

The Event Handler can be used to publish and subscribe to events between applications. Use the Admin API to configure namespaces, topics and subscriptions (as an alternative to using the Event Handler UI), and to get information about your subscriptions.

## Table of contents

<!--
Regenerate table of contents with:

npm install --global markdown-toc
markdown-toc -i --maxdepth 3 README.md
-->

<!-- toc -->

- [Links](#links)
- [Prerequisites](#prerequisites)
- [Example apps](#example-apps)
- [Code snippets](#code-snippets)
  * [Setup](#setup)
  * [Create topic](#create-topic)
  * [Create subscription](#create-subscription)
  * [Publish event](#publish-event)
  * [Delete subscription](#delete-subscription)
  * [Delete topic](#delete-topic)

<!-- tocstop -->

## Links

<!--ts-->
* [General info](https://acpaas.digipolis.be/nl/product/event-handler-engine)
* [Swagger documentation (API Store ACC)](https://api-store-a.antwerpen.be/#/org/acpaas/api/eventhandleradmin/v2/documentation)
* [Event Handler UI ACC](https://eventhandler-a.antwerpen.be/) - request access using the User Management Engine (UM)
* [Additional API documentation](https://bitbucket.antwerpen.be/projects/EVHA/repos/eventhandler-api_nodejs/browse/docs)
<!--te-->

## Prerequisites

- API key of an application which has a contract with the Event Handler Admin API in ACC.
- Event Handler owner key of an existing namespace. You can create a namespace through the Event Handler UI or ask the ACPaaS team to create one for you. A namespace owner key is not needed when you only need to create subscriptions.

## Example apps

- [.NET Core](example_dotnetcore)
- [Node.js](example_nodejs)

## Code snippets

These snippets and other examples are available in [EventHandlerAdminService.cs (.NET Core)](example_dotnetcore/EventHandlerAdminService.cs) and [eventHandlerAdminService.js (Node.js)](example_nodejs/eventHandlerAdminService.js).

### Setup

First configure the base URL, API key, namespace name, namespace owner key and subscription owner key.

*Note on owner keys:* In real implementations your service will probably be used for either publishing or subscribing to events. A namespace owner key is needed for creating namespaces, creating topics and publishing events, while a subscription owner key is needed for creating subscriptions, so you might not need both at the same time. These keys are self-chosen when creating a namespace or subscription.

**.NET Core:**

```csharp
public EventHandlerAdminService(string baseAddress, string apiKey, string namespaceName, string namespaceOwnerKey, string subscriptionOwnerKey)
{
    // Use IHttpClientFactory (AddHttpClient) in real implementations
    _httpClient = new HttpClient();
    _httpClient.BaseAddress = new Uri(baseAddress);
    _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);

    _namespaceName = namespaceName;
    _namespaceOwnerKey = namespaceOwnerKey;
    _subscriptionOwnerKey = subscriptionOwnerKey;
}
```

**Node.js:**

```js
class EventHandlerAdminService {
    constructor(config) {
        this.axiosClient = axios.create({
            baseURL: config.baseAddress,
            headers: { "ApiKey": config.apiKey }
        });

        this.namespace = config.namespace;
        this.namespaceOwnerKey = config.namespaceOwnerKey;
        this.subscriptionOwnerKey = config.subscriptionOwnerKey;
    }
}
```

### Create topic

**.NET Core:**

```csharp
public async Task<JObject> CreateTopic(string name)
{
    var body = JObject.FromObject(new
    {
        name = name
    });

    var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"namespaces/{_namespaceName}/topics");

    requestMessage.Headers.Add("owner-key", _namespaceOwnerKey);
    requestMessage.Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

    var responseMessage = await _httpClient.SendAsync(requestMessage);
    var responseContent = await responseMessage.Content.ReadAsStringAsync();

    if (!responseMessage.IsSuccessStatusCode)
    {
        throw new Exception($"Create topic failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"Create topic response ({(int)responseMessage.StatusCode}): {responseContent}");
    // Create topic response (201): {"name":"my-topic","namespace":"code-snippets-example-namespace"}

    return JObject.Parse(responseContent);
}
```

**Node.js:**

```js
async createTopic(name) {
    const response = await this.axiosClient.post(`/namespaces/${this.namespace}/topics`,
        { name },
        { headers: { "owner-key": this.namespaceOwnerKey } }
    );

    console.log(`CreateTopic response (${response.status}): ${JSON.stringify(response.data)}`);
    // CreateTopic response (201): {"name":"my-topic","namespace":"code-snippets-example-namespace"}

    return response.data;
}
```

### Create subscription

Subscriptions support advanced configuration options which are not covered in this snippet. You can configure retry strategies, HTTP headers, authentication, concurrent deliveries, etc. Kafka subscriptions are also supported in addition to the default HTTP subscriptions.

You can also get subscriptions metrics and errors, replay or delete errors, and pause or restart the subscription.

Please check the Swagger or the [additional API documentation](https://bitbucket.antwerpen.be/projects/EVHA/repos/eventhandler-api_nodejs/browse/docs) for more information.

**.NET Core:**

```csharp
public async Task<JObject> CreateSubscription(string topicName, string name, string url)
{
    var body = JObject.FromObject(new
    {
        name = name,
        topic = topicName,
        config = new
        {
            push = new
            {
                url = url
            }
        }
    });

    var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"namespaces/{_namespaceName}/subscriptions");

    requestMessage.Headers.Add("owner-key", _subscriptionOwnerKey);
    requestMessage.Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

    var responseMessage = await _httpClient.SendAsync(requestMessage);
    var responseContent = await responseMessage.Content.ReadAsStringAsync();

    if (!responseMessage.IsSuccessStatusCode)
    {
        throw new Exception($"Create subscription failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"Create subscription response ({(int)responseMessage.StatusCode}): {responseContent}");
    // Create subscription response (201): {"name":"my-subscription","namespace":"code-snippets-example-namespace","owner":"mySubscriptionOwnerKey","topic":"my-topic","config":{"maxConcurrentDeliveries":1,"retries":{"firstLevelRetries":{"enabled":true,"retries":3,"onFailure":"error"},"secondLevelRetries":{"enabled":false,"retries":10,"ttl":600,"onFailure":"error"}},"push":{"pushType":"http","httpVerb":"POST","authentication":{"type":"none","kafka":{},"basic":{},"apikey":{},"oauth":{}},"url":"http://localhost/some-subscription-endpoint"},"restartAfterStop":{"enabled":false,"delayInMinutes":30}},"updated":"2020-03-23T14:15:13.982Z","created":"2020-03-23T14:15:13.983Z","status":"active"}

    return JObject.Parse(responseContent);
}
```

**Node.js:**

```js
async createSubscription(topic, name, url) {
    const response = await this.axiosClient.post(`namespaces/${this.namespace}/subscriptions`,
        {
            name,
            topic,
            config: {
                push: {
                    url
                }
            }
        },
        { headers: { "owner-key": this.subscriptionOwnerKey } }
    );

    console.log(`CreateSubscription response (${response.status}): ${JSON.stringify(response.data)}`);
    // CreateSubscription response (201): {"name":"my-subscription","namespace":"code-snippets-example-namespace","owner":"mySubscriptionOwnerKey","topic":"my-topic","config":{"maxConcurrentDeliveries":1,"retries":{"firstLevelRetries":{"enabled":true,"retries":3,"onFailure":"error"},"secondLevelRetries":{"enabled":false,"retries":10,"ttl":600,"onFailure":"error"}},"push":{"pushType":"http","httpVerb":"POST","authentication":{"type":"none","kafka":{},"basic":{},"apikey":{},"oauth":{}},"url":"http://localhost/some-subscription-endpoint"},"restartAfterStop":{"enabled":false,"delayInMinutes":30}},"updated":"2020-03-23T18:39:19.527Z","created":"2020-03-23T18:39:19.528Z","status":"active"}

    return response.data;
}
```

### Publish event

**.NET Core:**

```csharp
public async Task Publish(string topicName, JObject content)
{
    var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"namespaces/{_namespaceName}/topics/{topicName}/publish");

    requestMessage.Headers.Add("owner-key", _namespaceOwnerKey);
    requestMessage.Content = new StringContent(content.ToString(), Encoding.UTF8, "application/json");

    var responseMessage = await _httpClient.SendAsync(requestMessage);

    if (!responseMessage.IsSuccessStatusCode)
    {
        var responseContent = await responseMessage.Content.ReadAsStringAsync();

        throw new Exception($"Publish failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"Publish response ({(int)responseMessage.StatusCode})");
    // Publish response (204)
}
```

**Node.js:**

```js
async publish(topic, content) {
    const response = await this.axiosClient.post(`namespaces/${this.namespace}/topics/${topic}/publish`,
        content,
        { headers: { "owner-key": this.namespaceOwnerKey } }
    );

    console.log(`Publish response (${response.status})`);
    // Publish response (204)
}
```

### Delete subscription

**.NET Core:**

```csharp
public async Task DeleteSubscription(string name)
{
    var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"namespaces/{_namespaceName}/subscriptions/{name}");

    requestMessage.Headers.Add("owner-key", _subscriptionOwnerKey);

    var responseMessage = await _httpClient.SendAsync(requestMessage);

    if (!responseMessage.IsSuccessStatusCode)
    {
        var responseContent = await responseMessage.Content.ReadAsStringAsync();

        throw new Exception($"Delete subscription failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"Delete subscription response ({(int)responseMessage.StatusCode})");
    // Delete subscription response (200)
}
```

**Node.js:**

```js
async deleteSubscription(name) {
    const response = await this.axiosClient.delete(`/namespaces/${this.namespace}/subscriptions/${name}`,
        { headers: { "owner-key": this.subscriptionOwnerKey } }
    );

    console.log(`DeleteSubscription response (${response.status})`);
    // DeleteSubscription response (200)
}
```

### Delete topic

**.NET Core:**

```csharp
public async Task DeleteTopic(string name)
{
    var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"namespaces/{_namespaceName}/topics/{name}");

    requestMessage.Headers.Add("owner-key", _namespaceOwnerKey);

    var responseMessage = await _httpClient.SendAsync(requestMessage);

    if (!responseMessage.IsSuccessStatusCode)
    {
        var responseContent = await responseMessage.Content.ReadAsStringAsync();

        throw new Exception($"Delete topic failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"Delete topic response ({(int)responseMessage.StatusCode})");
    // Delete topic response (200)
}
```

**Node.js:**

```js
async deleteTopic(name) {
    const response = await this.axiosClient.delete(`/namespaces/${this.namespace}/topics/${name}`,
        { headers: { "owner-key": this.namespaceOwnerKey } }
    );

    console.log(`DeleteTopic response (${response.status})`);
    // DeleteTopic response (200)
}
```


