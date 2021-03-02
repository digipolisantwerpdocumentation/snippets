# WCM Content Manager API examples

These examples show how to use the WCM Content Manager API.

This API should be used for managing content or content structures on your WCM tenant.

Apart from these examples, the API offers much more functionalities, so make sure to check the Swagger and documentation.

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
  * [Setup](#setup)
  * [Get application](#get-application)
  * [Get application roles](#get-application-roles)
  * [Get role teams](#get-role-teams)
  * [Get team subjects](#get-team-subjects)

<!-- tocstop -->

## Links

- ACPaaS Portal:
  * [Web Content Management engine (WCM)](https://acpaas.digipolis.be/nl/product/web-content-management-engine/about)
  * [Getting started](https://acpaas.digipolis.be/nl/product/web-content-management-engine/v3.5.1/gettingStarted)
  * [WCM Content Manager API](https://acpaas.digipolis.be/nl/product/web-content-management-engine/v3.5.1/api-web-content-manager-engine-v-3/about)
- [API Store ACC (Swagger)](https://api-store-a.antwerpen.be/#/org/acpaas/api/wcmcontentmanager/v3/documentation)

## Example apps

- [.NET Core](example_dotnetcore)
- [Node.js](example_nodejs)

## Code snippets

These snippets are available in [WcmService.cs (.NET Core)](example_dotnetcore/WcmService.cs) and [wcmService.js (Node.js)](example_nodejs/wcmService.js).

### Setup

First configure the base URL and API key:

**.NET Core:**

```csharp
public class WcmService
{
    private readonly HttpClient _httpClient;

    public WcmService(string baseAddress, string apiKey, string tenant)
    {
        // Use IHttpClientFactory (AddHttpClient) in real implementations
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(baseAddress);
        _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
        _httpClient.DefaultRequestHeaders.Add("tenant", tenant);
    }
}
```

**Node.js:**

```js
class WcmService {
  constructor(config) {
    this.axiosInstance = axios.create({
      baseURL: config.baseAddress,
      headers: {
        ApiKey: config.apiKey,
        tenant: config.tenant,
      },
    });
  }
}
```

### Create content item

`POST https://api-gw-a.antwerpen.be/acpaas/wcm-content-manager/v3/content`

Example payload:

```json
{
  "meta": {
    "activeLanguages": ["NL"],
    "contentType": "5b921066534a07e22c43aece",
    "publishDate": "2020-10-20T11:00:00.000Z",
    "slug": {
      "multiLanguage": true,
      "NL": "test-code-snippet"
    },
    "label": "TestCodeSnippet",
    "status": "PUBLISHED"
  },
  "fields":{
  	"test": "Some test"
  }
}
```

Response:

```json
{
  "_id": "6037bced15a804d1f0765305",
  "fields": {
    "test": "Some test"
  },
  "meta": {
    "contentType": {
      "_id": "5b921066534a07e22c43aece",
      "versions": [],
      "fields": [
        {
          "_id": "test",
          "validation": {
            "required": false
          },
          "type": "text",
          "label": "test",
          "operators": [
            {
              "label": "equals",
              "value": "equals"
            },
            {
              "label": "contains",
              "value": "i"
            },
            {
              "label": "starts with",
              "value": "^"
            },
            {
              "label": "ends with",
              "value": "$"
            }
          ],
          "dataType": "string",
          "indexed": false,
          "multiLanguage": false,
          "options": [],
          "max": 1,
          "min": 1,
          "taxonomyLists": [],
          "uuid": "27357f73-91ea-4e0c-89f2-f1a0d079f7d6"
        }
      ],
      "meta": {
        "label": "testken",
        "description": "test ken",
        "safeLabel": "testken",
        "lastEditor": "5a002a269062ab6b7212d2dd",
        "canBeFiltered": true,
        "deleted": false,
        "hitCount": 0,
        "taxonomy": {
          "tags": [],
          "fieldType": "Taxonomy",
          "available": []
        },
        "lastModified": "2018-09-07T05:45:10.602Z",
        "created": "2018-09-07T05:45:10.602Z"
      },
      "uuid": "39ea4178-3fa1-4d8d-b2f0-3a4285c3de2c",
      "__v": 0
    },
    "publishDate": "2020-10-20T11:00:00.000Z",
    "label": "TestCodeSnippet",
    "status": "PUBLISHED",
    "safeLabel": "test_code_snippet",
    "lastEditor": "111111111111111111111111",
    "firstPublished": "2021-02-25T15:06:21.079Z",
    "parents": {
      "views": [],
      "content": []
    },
    "deleted": false,
    "hasDetail": false,
    "activeLanguages": [
      "NL"
    ],
    "hitCount": 0,
    "hasScheduled": false,
    "published": true,
    "lastModified": "2021-02-25T15:06:21.077Z",
    "created": "2021-02-25T15:06:21.077Z",
    "taxonomy": {
      "tags": [],
      "dataType": "taxonomy",
      "available": []
    },
    "slug": {
      "NL": "test-code-snippet",
      "multiLanguage": true
    }
  },
  "uuid": "5d394e32-6e30-48b2-8994-7e25f227c115",
  "__v": 0
}
```

#### Example implementation

**.NET Core:**

```csharp
public async Task<JObject> CreateContentItem(JObject content)
{
    HttpContent requestContent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
    var responseMessage = await _httpClient.PostAsync("content", requestContent);
    var responseContent = await responseMessage.Content.ReadAsStringAsync();

    if (!responseMessage.IsSuccessStatusCode)
    {
        throw new Exception($"CreateContentItem failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"CreateContentItem response ({(int)responseMessage.StatusCode}): {responseContent}");
    // CreateContentItem response (201): {"_id":"6037bc5215a804d1f07652d6","fields":{"test":"Some test"},"meta":{"contentType":{"_id":"5b921066534a07e22c43aece","versions":[],"fields":[{"_id":"test","validation":{"required":false},"type":"text","label":"test","operators":[{"label":"equals","value":"equals"},{"label":"contains","value":"i"},{"label":"starts with","value":"^"},{"label":"ends with","value":"$"}],"dataType":"string","indexed":false,"multiLanguage":false,"options":[],"max":1,"min":1,"taxonomyLists":[],"uuid":"27357f73-91ea-4e0c-89f2-f1a0d079f7d6"}],"meta":{"label":"testtype","description":"test type","safeLabel":"testtype","lastEditor":"5a002a269062ab6b7212d2dd","canBeFiltered":true,"deleted":false,"hitCount":0,"taxonomy":{"tags":[],"fieldType":"Taxonomy","available":[]},"lastModified":"2018-09-07T05:45:10.602Z","created":"2018-09-07T05:45:10.602Z"},"uuid":"39ea4178-3fa1-4d8d-b2f0-3a4285c3de2c","__v":0},"publishDate":"2020-10-20T11:00:00.000Z","label":"TestCodeSnippet","status":"PUBLISHED","safeLabel":"test_code_snippet","lastEditor":"111111111111111111111111","firstPublished":"2021-02-25T15:03:46.604Z","parents":{"views":[],"content":[]},"deleted":false,"hasDetail":false,"activeLanguages":["NL"],"hitCount":0,"hasScheduled":false,"published":true,"lastModified":"2021-02-25T15:03:46.604Z","created":"2021-02-25T15:03:46.604Z","taxonomy":{"tags":[],"dataType":"taxonomy","available":[]},"slug":{"NL":"test-code-snippet","multiLanguage":true}},"uuid":"a22ec337-ac77-4fc2-8eb0-3bc12d716daf","__v":0}

    return JObject.Parse(responseContent);
}
```

**Node.js:**

```js
async createContentItem(content) {
  const response = await this.axiosInstance.post('content', content);
  console.log(`createContentItem response (${response.status}): ${JSON.stringify(response.data)}`);
  // createContentItem response (201): {"_id":"6037bced15a804d1f0765305","fields":{"test":"Some test"},"meta":{"contentType":{"_id":"5b921066534a07e22c43aece","versions":[],"fields":[{"_id":"test","validation":{"required":false},"type":"text","label":"test","operators":[{"label":"equals","value":"equals"},{"label":"contains","value":"i"},{"label":"starts with","value":"^"},{"label":"ends with","value":"$"}],"dataType":"string","indexed":false,"multiLanguage":false,"options":[],"max":1,"min":1,"taxonomyLists":[],"uuid":"27357f73-91ea-4e0c-89f2-f1a0d079f7d6"}],"meta":{"label":"testtype","description":"test type","safeLabel":"testtype","lastEditor":"5a002a269062ab6b7212d2dd","canBeFiltered":true,"deleted":false,"hitCount":0,"taxonomy":{"tags":[],"fieldType":"Taxonomy","available":[]},"lastModified":"2018-09-07T05:45:10.602Z","created":"2018-09-07T05:45:10.602Z"},"uuid":"39ea4178-3fa1-4d8d-b2f0-3a4285c3de2c","__v":0},"publishDate":"2020-10-20T11:00:00.000Z","label":"TestCodeSnippet","status":"PUBLISHED","safeLabel":"test_code_snippet","lastEditor":"111111111111111111111111","firstPublished":"2021-02-25T15:06:21.079Z","parents":{"views":[],"content":[]},"deleted":false,"hasDetail":false,"activeLanguages":["NL"],"hitCount":0,"hasScheduled":false,"published":true,"lastModified":"2021-02-25T15:06:21.077Z","created":"2021-02-25T15:06:21.077Z","taxonomy":{"tags":[],"dataType":"taxonomy","available":[]},"slug":{"NL":"test-code-snippet","multiLanguage":true}},"uuid":"5d394e32-6e30-48b2-8994-7e25f227c115","__v":0}

  return response.data;
}
```

### Get content item

`GET https://api-gw-a.antwerpen.be/acpaas/wcm-content-manager/v3/content/{uuid}`

Response:

```json
{
  "_id": "6037bced15a804d1f0765305",
  "fields": {
    "test": "Some test"
  },
  "meta": {
    "publishDate": "2020-10-20T11:00:00.000Z",
    "label": "TestCodeSnippet",
    "status": "PUBLISHED",
    "safeLabel": "test_code_snippet",
    "firstPublished": "2021-02-25T15:06:21.079Z",
    "parents": {
      "views": [],
      "content": []
    },
    "deleted": false,
    "hasDetail": false,
    "activeLanguages": [
      "NL"
    ],
    "hitCount": 0,
    "hasScheduled": false,
    "published": true,
    "lastModified": "2021-02-25T15:06:21.077Z",
    "created": "2021-02-25T15:06:21.077Z",
    "taxonomy": {
      "tags": []
    },
    "slug": "test-code-snippet",
    "historyRef": "efaea42b-9cfd-435e-8390-37b495221921",
    "contentType": "testtype",
    "contentTypeUuid": "39ea4178-3fa1-4d8d-b2f0-3a4285c3de2c"
  },
  "uuid": "5d394e32-6e30-48b2-8994-7e25f227c115"
}
```

#### Example implementation

**.NET Core:**

```csharp
public async Task<JObject> GetContentItem(string uuid)
{
    var query = HttpUtility.ParseQueryString(string.Empty);
    query["lang"] = "NL";
    query["populate"] = "true";

    var responseMessage = await _httpClient.GetAsync($"content/{uuid}?{query}");
    var responseContent = await responseMessage.Content.ReadAsStringAsync();

    if (!responseMessage.IsSuccessStatusCode)
    {
        throw new Exception($"GetContentItem failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"GetContentItem response ({(int)responseMessage.StatusCode}): {responseContent}");
    // {"_id":"6037bc5215a804d1f07652d6","fields":{"test":"Some test"},"meta":{"publishDate":"2020-10-20T11:00:00.000Z","label":"TestCodeSnippet","status":"PUBLISHED","safeLabel":"test_code_snippet","firstPublished":"2021-02-25T15:03:46.604Z","parents":{"views":[],"content":[]},"deleted":false,"hasDetail":false,"activeLanguages":["NL"],"hitCount":0,"hasScheduled":false,"published":true,"lastModified":"2021-02-25T15:03:46.604Z","created":"2021-02-25T15:03:46.604Z","taxonomy":{"tags":[]},"slug":"test-code-snippet","historyRef":"381f124a-178e-4e13-b746-e6383d282985","contentType":"testtype","contentTypeUuid":"39ea4178-3fa1-4d8d-b2f0-3a4285c3de2c"},"uuid":"a22ec337-ac77-4fc2-8eb0-3bc12d716daf"}

    return JObject.Parse(responseContent);
}
```

**Node.js:**

```js
async getContentItem(uuid) {
  const response = await this.axiosInstance.get(`content/${uuid}`, {
    params: {
      lang: 'NL',
      populate: true,
    },
  });
  console.log(`getContentItem response (${response.status}): ${JSON.stringify(response.data)}`);
  // getContentItem response (200): {"_id":"6037bced15a804d1f0765305","fields":{"test":"Some test"},"meta":{"publishDate":"2020-10-20T11:00:00.000Z","label":"TestCodeSnippet","status":"PUBLISHED","safeLabel":"test_code_snippet","firstPublished":"2021-02-25T15:06:21.079Z","parents":{"views":[],"content":[]},"deleted":false,"hasDetail":false,"activeLanguages":["NL"],"hitCount":0,"hasScheduled":false,"published":true,"lastModified":"2021-02-25T15:06:21.077Z","created":"2021-02-25T15:06:21.077Z","taxonomy":{"tags":[]},"slug":"test-code-snippet","historyRef":"efaea42b-9cfd-435e-8390-37b495221921","contentType":"testtype","contentTypeUuid":"39ea4178-3fa1-4d8d-b2f0-3a4285c3de2c"},"uuid":"5d394e32-6e30-48b2-8994-7e25f227c115"}

  return response.data;
}
```

### Delete content item

`DELETE https://api-gw-a.antwerpen.be/acpaas/wcm-content-manager/v3/content/{uuid}`

#### Example implementation

**.NET Core:**

```csharp
public async Task<JObject> DeleteContentItem(string uuid)
{
    var responseMessage = await _httpClient.DeleteAsync($"content/{uuid}");
    var responseContent = await responseMessage.Content.ReadAsStringAsync();

    if (!responseMessage.IsSuccessStatusCode)
    {
        throw new Exception($"DeleteContentItem failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"DeleteContentItem response ({(int)responseMessage.StatusCode})");
    // 

    return JObject.Parse(responseContent);
}
```

**Node.js:**

```js
async deleteContentItem(uuid) {
  const response = await this.axiosInstance.delete(`content/${uuid}`);
  console.log(`deleteContentItem response (${response.status})`);
  // deleteContentItem response (204)

  return response.data;
}
```
