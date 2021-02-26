#  WCM Proxy

These examples show how to use the WCM Proxy API.

This (mostly read-only) API can be used for creating a consumer application while managing the content through the WCM Editor.

## Table of contents

<!--
Regenerate table of contents with:

npm install --global markdown-toc
markdown-toc -i --maxdepth 3 README.md
-->

<!-- toc -->

- [Links](#links)
- [Example apps](#example-apps)
- [Code snippets](#code-snippets)
  * [Setup (.NET Core)](#setup-net-core)
  * [Get view](#get-view)
  * [Get content item](#get-content-item)
  * [BFF proxy (Node.js)](#bff-proxy-nodejs)

<!-- tocstop -->

## Links

- ACPaaS Portal:
  * [Web Content Management engine (WCM)](https://acpaas.digipolis.be/nl/product/web-content-management-engine/about)
  * [Getting started](https://acpaas.digipolis.be/nl/product/web-content-management-engine/v3.5.1/gettingStarted)
  * [WCM Proxy API](https://acpaas.digipolis.be/nl/product/web-content-management-engine/v3.5.1/api-web-content-manager-proxy-v-3/about)
- [API Store ACC (Swagger)](https://api-store-a.antwerpen.be/#/org/acpaas/api/wcmproxy/v3/documentation)

## Example apps

- [.NET Core](example_dotnetcore)
- Node.js snippets are provided inline in this readme

## Code snippets

### Setup (.NET Core)

First configure the base URL, API key and tenant:

```csharp
public class WcmProxyService
{
    private readonly HttpClient _httpClient;

    public WcmProxyService(string baseAddress, string apiKey, string tenant)
    {
        // Use IHttpClientFactory (AddHttpClient) in real implementations
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(baseAddress);
        _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
        _httpClient.DefaultRequestHeaders.Add("tenant", tenant);
    }
}
```

### Get view

`GET https://api-gw-a.antwerpen.be/acpaas/wcm-proxy/v3/views?uuid={uuid}`

#### Example implementation (.NET Core)

```csharp
public async Task<JObject> GetView(string uuid)
{
    var query = HttpUtility.ParseQueryString(string.Empty);
    query["uuid"] = uuid;
    query["lang"] = "nl";
    query["populate"] = "true";
    query["skip"] = "0";
    query["limit"] = "10";

    var responseMessage = await _httpClient.GetAsync($"views?{query}");
    var responseContent = await responseMessage.Content.ReadAsStringAsync();

    if (!responseMessage.IsSuccessStatusCode)
    {
        throw new Exception($"GetView failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"GetView response ({(int)responseMessage.StatusCode}): {responseContent}");

    return JObject.Parse(responseContent);
}
```

### Get content item

`GET https://api-gw-a.antwerpen.be/acpaas/wcm-proxy/v3/content?uuid={uuid}`

#### Example implementation

**.NET Core:**

```csharp
public async Task<JObject> GetContentItem(string uuid)
{
    var query = HttpUtility.ParseQueryString(string.Empty);
    query["uuid"] = uuid;
    query["lang"] = "nl";
    query["populate"] = "true";

    var responseMessage = await _httpClient.GetAsync($"content?{query}");
    var responseContent = await responseMessage.Content.ReadAsStringAsync();

    if (!responseMessage.IsSuccessStatusCode)
    {
        throw new Exception($"GetContentItem failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"GetContentItem response ({(int)responseMessage.StatusCode}): {responseContent}");

    return JObject.Parse(responseContent);
}
```

**Node.js:**

```javascript
const request = require('request-promise-native');

const config = {
    host: 'https://api-gw-a.antwerpen.be',
    baseUrl: '/acpaas/wcm-proxy/v3',
}

async function getContent($page) {
    try {
        const options = {
            json: true,
            resolveWithFullResponse: true,
            url: `${config.host}${config.baseUrl}/content?slug=${page}&populate=2&lang=nl`,
        };

        const response = await request.get(options);
    } catch (error) {
        console.log(`getContent failed: ${error.message}`);
    }

}

getContent('home');
```
### BFF proxy (Node.js)

This Node.js example shows how to setup a proxy router in your BFF (Backend-For-Frontend). This the frontend has access to all WCM endpoints without needing authentication details. This example adds the ApiKey header to WCM requests.

```javascript
import proxy from 'express-http-proxy';
import url from 'url';

const apiUrl = url.parse(process.env.WCM_BASESURL);
const router = proxy(process.env.WCM_BASESURL, {
  proxyReqPathResolver: (req) => {
    const reqUrl = url.parse(req.url);
    return apiUrl.path + reqUrl.path;
  },
  proxyReqOptDecorator: async (proxyReq, req) => {
    try {
      const copy = proxyReq;
      copy.headers.apikey = process.env.WCM_APIKEY;
      copy.headers.tenant = process.env.WCM_TENNANT;
      return copy;
    } catch (error) {
      console.log(error);
      return proxyReq;
    }
  },
});

export default router;
```

*A similar implementation is possible in .NET but is not provided as a snippet.*
