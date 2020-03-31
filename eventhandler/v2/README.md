![eventhandler](./assets/eventhandler.png)

# Event Handler

The Event Handler can be used to publish and receive events that can be used by other applications.

## Table of contents

<!--
Regenerate table of contents with:

npm install --global markdown-toc
markdown-toc -i --maxdepth 3 README.md
-->

<!-- toc -->

- [Links](#links)
- [Publish event](#publish-event)
  * [Publish code example](#publish-code-example)
- [Subscribe to an event](#subscribe-to-an-event)
  * [Receive event code example](#receive-event-code-example)

<!-- tocstop -->

## Links

* [General info](https://acpaas.digipolis.be/nl/product/event-handler-engine)
* [User manual](https://wiki.antwerpen.be/ACPAAS/index.php/Event-Handler_User_Manuals) *(internal access needed)*
* [Swagger documentation](https://acpaas.digipolis.be/nl/product/event-handler-engine/v2.0.0/api-event-handler-v-2/about)

## Publish event

**API documentation:** [Swagger](https://acpaas.digipolis.be/nl/product/event-handler-engine/v2.0.0/api-event-handler-v-2/about#/Publish)

### Publish code example

**.NET Core:**

```csharp
class EventHandlerService
{
    private readonly HttpClient _httpClient;
    private readonly string _namespace;

    public EventHandlerService(string baseAddress, string apiKey, string ownerKey, string namespaceName)
    {
        // Use IHttpClientFactory (AddHttpClient) in real implementations
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(baseAddress);
        _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
        _httpClient.DefaultRequestHeaders.Add("Owner-Key", ownerKey);

        _namespace = namespaceName;
    }

    public async Task Publish(string topic, string content)
    {
        var responseMessage = await _httpClient.PostAsync($"namespaces/{_namespace}/topics/{topic}/publish", new StringContent(content));

        if (!responseMessage.IsSuccessStatusCode)
        {                
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            throw new Exception($"Publish failed ({(int)responseMessage.StatusCode}): {responseContent}");
        }

        Console.WriteLine($"Publish response ({(int)responseMessage.StatusCode})");
        // Publish response (204)
    }
}
```

**Node.js:**

```javascript
const request = require('request-promise-native');

const config = {
    host: 'https://api-gw-o.antwerpen.be', // Dev endpoint
    baseUrl: '/acpaas/eventhandler/v2',
    ownerKey: '[OWNERKEY]',
    namespace: '[NAMESPACE]',
    apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the api-store https://api-store-o.antwerpen.be/
}

async function sendEvent(topic, message) {
    try {
        const options = {
            body: message,
            headers: {
                'owner-key': config.ownerKey,
                apikey: config.apiKey
            },
            json: true,
            resolveWithFullResponse: true,
            url: `${config.host}${config.baseUrl}/namespaces/${config.namespace}/topics/${topic}/publish`,
        };
        const response = await request.post(options);
        if (response.statusCode !== 204) {
            console.log(`Success`);
        }

    } catch (error) {
        console.log(`Post error ${error.message} from event handler`);
    }

}
sendEvent('[TOPIC]', '{ datakey: "datavalue"}');
```
## Subscribe to an event

Create a subscription in the Event Handler that points to your application: [User manual](https://wiki.antwerpen.be/ACPAAS/index.php/Event-Handler_User_Manuals) *(internal access needed)*.

The Event Handler will post data to an endpoint. Return a success status code when handling the event succeeded or a failure status code otherwise.

Consider securing this endpoint using authentication or a secret HTTP header, which can be set on the Event Handler subscription.

### Receive event code example

**.NET Core:**

```csharp
[ApiController]
[Route("[controller]")]
public class EventHandlerSubscriptionController : ControllerBase
{
    [HttpPost]
    public IActionResult HandleEvent([FromBody] ExamplePayload payload)
    {     
        Console.WriteLine($"Handling event with payload ID \"{payload.Id}\"");

        // Do something

        // Send success
        return NoContent();
    }

    // The payload depends on the event you're subscribing to
    public class ExamplePayload
    {
        public string Id { get; set; }
    }
}
```

**Node.js:**

```javascript
// Express route with bodyparser expected

router.post('/message', handleMessage);

function handleMessage(req, res, next) {
  try {
    console.log(`received message ${body}`)
    // Do something
    // Send success
    return res.json();
  } catch (err) {
    console.log(`HandleMessage Error: ${err}`);
    return next(err);
  }
}
```
