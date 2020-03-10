# Assets API examples

## Table of contents

<!--
Regenerate table of contents with:

npm install --global markdown-toc
markdown-toc -i --maxdepth 3 assets/v1/README.md
-->

<!-- toc -->

- [Links](#links)
- [Example apps](#example-apps)
- [Code snippets](#code-snippets)
  * [Setup](#setup)
  * [Create upload ticket](#create-upload-ticket)
  * [Upload file](#upload-file)
  * [Get mediafile URL](#get-mediafile-url)
  * [Set ACL](#set-acl)
  * [Delete file](#delete-file)

<!-- tocstop -->

## Links

- [ACPaaS Portal](https://acpaas.digipolis.be/nl/product/digital-assets-engine)
- [API Store ACC](https://api-store-a.antwerpen.be/#/org/inuits/api/assets/v1/documentation)
- [ACPaaS Wiki page](https://wiki.antwerpen.be/ACPAAS/index.php/Digital_Asset_express_engine)

## Example apps

- [.NET Core](example_dotnetcore)
- [Node.js](example_nodejs)

## Code snippets

These snippets and other examples are available in [AssetsService.cs (.NET Core)](example_dotnetcore/AssetsService.cs) and [assetsService.js (Node.js)](example_nodejs/assetsService.js).

### Setup

First configure the base URL and API key:

**.NET Core:**

```csharp
public AssetsService(string baseAddress, string apiKey)
{
    // Use IHttpClientFactory (AddHttpClient) in real implementations
    _httpClient = new HttpClient();
    _httpClient.BaseAddress = new Uri(baseAddress);
    _httpClient.DefaultRequestHeaders.Add("apikey", apiKey);
}
```

**Node.js:**

```js
class AssetsService {
    constructor(config) {
        this.requestClient = requestPromise.defaults({
            baseUrl: config.baseAddress,
            headers: { "apikey": config.apiKey },
            resolveWithFullResponse: true
        });
    }
}
```

Add a method to parse the result XML:

**.NET Core:**

```csharp
private async Task<JObject> ParseResult(HttpResponseMessage response, string method)
{
    var responseXml = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
    {
        throw new Exception($"{method} failed ({(int)response.StatusCode}): {responseXml}");
    }

    Console.WriteLine($"{method} response ({(int)response.StatusCode}): {responseXml}");

    // Parse XML to Json.NET JObject (consider deserializing to custom classes in real implementations)
    var xml = new XmlDocument();
    xml.LoadXml(responseXml);

    var parsedResult = JObject.Parse(JsonConvert.SerializeObject(xml));

    if (parsedResult.SelectToken("response.header.request_result").ToString() == "error")
    {
        // You should probably throw an exception in real implementations
        Console.WriteLine($"{method} failed: {parsedResult.SelectToken("response.header.request_result_description")}");
    }

    return parsedResult;
}
```

**Node.js:**

```js
const parseXmlString = require("xml2js").parseStringPromise;

async parseResult(response, method) {
    console.log(`${method} response (${response.statusCode}): ${response.body}`);

    const parsedResult = await parseXmlString(response.body, { explicitArray: false });

    if (parsedResult.response.header.request_result === "error") {
        // You should probably throw an exception in real implementations
        console.log(`${method} failed: ${parsedResult.response.header.request_result_description}`);
    }

    return parsedResult;
}
```

### Create upload ticket

`POST /upload/ticket/create`

Response:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<response xmlns:dc="http://purl.org/dc/elements/1.1/">
  <header>
    <item_count>1</item_count>
    <item_count_total>1</item_count_total>
    <item_offset>0</item_offset>
    <request_class>mediamosa_rest_call_upload_ticket_create</request_class>
    <request_matched_method>POST</request_matched_method>
    <request_matched_uri>/upload/ticket/create</request_matched_uri>
    <request_result>success</request_result>
    <request_result_description></request_result_description>
    <request_result_id>601</request_result_id>
    <request_uri>[POST] upload/ticket/create</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.0375</request_process_time>
  </header>
  <items>
    <item>
      <action>https://media-a.antwerpen.be/mediafile/upload?upload_ticket=W9Dhe4sU9F8CAYYirmFwFvK0</action>
      <uploadprogress_url>https://media-a.antwerpen.be/uploadprogress?id=9980626</uploadprogress_url>
      <asset_id>u1mkXRZCcJNEaXb5JMtqrH3K</asset_id>
      <mediafile_id>O2RWXgWpZXdXHUmVYQ8afrRv</mediafile_id>
      <ticket_id>W9Dhe4sU9F8CAYYirmFwFvK0</ticket_id>
      <progress_id>9980626</progress_id>
      <server_id>131</server_id>
    </item>
  </items>
</response>
```

#### Example implementation

**.NET Core:**

```csharp
public async Task<JObject> CreateUploadTicket(string userId)
{
    var requestContent = new MultipartFormDataContent();

    requestContent.Add(new StringContent(userId), "user_id");
    // The user_id property can be used to specify the user who uploaded the file
    // The same user ID is also needed for other calls concerning the same file

    requestContent.Add(new StringContent("true"), "isprivate");
    requestContent.Add(new StringContent("true"), "is_downloadable");
    requestContent.Add(new StringContent("true"), "published");

    var response = await _httpClient.PostAsync("upload/ticket/create", requestContent);

    return await ParseResult(response, "CreateUploadTicket");
}
```

**Node.js:**

```js
async createUploadTicket(userId) {
    const formData = {
        user_id: userId
        // The user_id property can be used to specify the user who uploaded the file
        // The same user ID is also needed for other calls concerning the same file
    };

    const response = await this.requestClient.post("upload/ticket/create", { formData });

    return await this.parseResult(response, "CreateUploadTicket");
}
```

### Upload file

`POST /mediafile/upload`

Response:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<response xmlns:dc="http://purl.org/dc/elements/1.1/">
  <header>
    <item_count>0</item_count>
    <item_count_total>0</item_count_total>
    <item_offset>0</item_offset>
    <request_class>mediamosa_rest_call_asset_mediafile_upload</request_class>
    <request_matched_method>POST</request_matched_method>
    <request_matched_uri>/mediafile/upload</request_matched_uri>
    <request_result>success</request_result>
    <request_result_description></request_result_description>
    <request_result_id>601</request_result_id>
    <request_uri>[POST] mediafile/upload</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.5721</request_process_time>
  </header>
  <items/>
</response>
```

#### Example implementation

**.NET Core:**

```csharp
public async Task<JObject> UploadFile(string ticketId, Stream stream, string fileName, bool createThumbnail, int? thumbnailSize)
{
    var requestContent = new MultipartFormDataContent();

    requestContent.Add(new StringContent(ticketId), "upload_ticket");
    requestContent.Add(new StringContent(createThumbnail.ToString().ToLower()), "create_still");

    if (thumbnailSize.HasValue)
    {
        requestContent.Add(new StringContent(thumbnailSize.ToString()), "size");
    }

    using (var streamContent = new StreamContent(stream))
    {
        // Filename normally doesn't contain double quotes but remove them just to be sure
        var headerFileName = fileName.Replace("\"", "");
        var contentDispositionHeader = $"form-data; name=\"file\"; filename=\"{headerFileName}\"";

        // UTF-8 conversion to handle special characters in filename
        contentDispositionHeader = new string(Encoding.UTF8.GetBytes(contentDispositionHeader).Select(b => (char)b).ToArray());

        streamContent.Headers.Add("Content-Disposition", contentDispositionHeader);

        streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        requestContent.Add(streamContent);

        var response = await _httpClient.PostAsync("mediafile/upload", requestContent);

        return await ParseResult(response, "UploadFile");
    };
}
```

**Node.js:**

```js
async uploadFile(ticketId, file, fileName, createThumbnail = false, thumbnailSize = null) {
    const formData = {
        upload_ticket: ticketId,
        create_still: createThumbnail.toString(),
        file: {
            value: file,
            options: {
                filename: fileName,
                contentType: "application/octet-stream"
            }
        }
    };

    if (thumbnailSize != null) {
        formData.size = thumbnailSize;
    }

    const response = await this.requestClient.post("mediafile/upload", { formData });

    return await this.parseResult(response, "UploadFile");
}
```

### Get mediafile URL

`GET /asset/{assetId}/view?user_id={userId}&response={responseType}&mediafile_id={mediafileId}`

Response:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<response xmlns:dc="http://purl.org/dc/elements/1.1/">
  <header>
    <item_count>1</item_count>
    <item_count_total>1</item_count_total>
    <item_offset>0</item_offset>
    <request_class>mediamosa_rest_call_media_view</request_class>
    <request_matched_method>GET</request_matched_method>
    <request_matched_uri>/asset/$asset_id/view</request_matched_uri>
    <request_result>success</request_result>
    <request_result_description></request_result_description>
    <request_result_id>601</request_result_id>
    <request_uri>[GET] asset/u1mkXRZCcJNEaXb5JMtqrH3K/view?user_id=my-user-id&amp;response=download&amp;mediafile_id=O2RWXgWpZXdXHUmVYQ8afrRv</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.0381</request_process_time>
  </header>
  <items>
    <item>
      <output>https://media-a.antwerpen.be/download/15/O/O2RWXgWpZXdXHUmVYQ8afrRv/image.png</output>
      <content_type>image/png</content_type>
      <ticket_id>O2RWXgWpZXdXHUmVYQ8afrRv</ticket_id>
    </item>
  </items>
</response>
```

The {{response}} query string parameter should be "download", "plain" or "still".

You should poll for the "still" (thumbnail) URL as it's not instantly available.

#### Example implementation

**.NET Core:**

```csharp
public async Task<JObject> GetMediafileView(string assetId, string userId, string responseType, string mediafileId)
{
    var responseMessage = await _httpClient.GetAsync($"asset/{assetId}/view?user_id={userId}&response={responseType}&mediafile_id={mediafileId}");

    return await ParseResult(responseMessage, $"GetMediafileView (user_id={userId}, response={responseType})");
}
```

**Node.js:**

```js
  async getMediafileView(assetId, userId, responseType, mediafileId) {
      const response = await this.requestClient.get(`asset/${assetId}/view?user_id=${userId}&response=${responseType}&mediafile_id=${mediafileId}`);

      return await this.parseResult(response, `GetMediafileView (user_id=${userId}, response=${responseType})`);
  }
```

### Set ACL

By default mediafiles are not protected by user ID and mediafile URLs are permanent. It might be necessary to conceal these URLs from end users, depending on your use case. Use Access Control Lists to limit access to a specific mediafile. Mediafile URLs will be temporary when using ACLs.

`POST /mediafile/{mediafileId}/acl`

Response:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<response xmlns:dc="http://purl.org/dc/elements/1.1/">
  <header>
    <item_count>1</item_count>
    <item_count_total>1</item_count_total>
    <item_offset>0</item_offset>
    <request_class>mediamosa_rest_call_acl_mediafile_set_rights</request_class>
    <request_matched_method>POST</request_matched_method>
    <request_matched_uri>/mediafile/$mediafile_id/acl</request_matched_uri>
    <request_result>success</request_result>
    <request_result_description></request_result_description>
    <request_result_id>601</request_result_id>
    <request_uri>[POST] mediafile/O2RWXgWpZXdXHUmVYQ8afrRv/acl</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.0773</request_process_time>
  </header>
  <items>
    <item>
      <acl_user>
        <value>second-user-id</value>
        <result>success</result>
        <result_id>601</result_id>
        <result_description></result_description>
      </acl_user>
    </item>
  </items>
</response>
```

Please refer to the [MediaMosa documentation](https://github.com/mediamosa/mediamosa/wiki/Authorization) for more information about ACLs.

A configuration setting ("Force temporary mediafile links for mediafiles") per application is also available to generate only temporary URLs (regardless of the use of ACLs). Please contact the ACPaaS team for more information if needed.

#### Example implementation

**.NET Core:**

```csharp
public async Task<JObject> SetMediafileAcl(string mediafileId, string userId, string aclUserId = null, string aclGroupId = null, string aclDomain = null, string aclRealm = null)
{
    var requestContent = new MultipartFormDataContent();

    requestContent.Add(new StringContent(userId), "user_id");

    if (aclUserId != null) {
        requestContent.Add(new StringContent(aclUserId), "acl_user_id");
    }

    if (aclGroupId != null) {
        requestContent.Add(new StringContent(aclGroupId), "acl_group_id");
    }

    if (aclDomain != null) {
        requestContent.Add(new StringContent(aclDomain), "acl_domain");
    }

    if (aclRealm != null) {
        requestContent.Add(new StringContent(aclRealm), "acl_realm");
    }

    var response = await _httpClient.PostAsync($"mediafile/{mediafileId}/acl", requestContent);

    return await ParseResult(response, "SetMediafileAcl");
}
```

**Node.js:**

```js
async setMediafileAcl(mediafileId, userId, aclUserId = null, aclGroupId = null, aclDomain = null, aclRealm = null) {
    const formData = {
        user_id: userId
    };

    if (aclUserId != null) {
        formData.acl_user_id = aclUserId;
    }

    if (aclGroupId != null) {
        formData.acl_group_id = aclGroupId;
    }

    if (aclDomain != null) {
        formData.acl_domain = aclDomain;
    }

    if (aclRealm != null) {
        formData.acl_realm = aclRealm;
    }

    const response = await this.requestClient.post(`mediafile/${mediafileId}/acl`, { formData });

    return await this.parseResult(response, "SetMediafileAcl");
}
```

### Delete file

`POST /asset/{assetId}/delete`

Response:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<response xmlns:dc="http://purl.org/dc/elements/1.1/">
  <header>
    <item_count>0</item_count>
    <item_count_total>0</item_count_total>
    <item_offset>0</item_offset>
    <request_class>mediamosa_rest_call_asset_delete</request_class>
    <request_matched_method>POST</request_matched_method>
    <request_matched_uri>/asset/$asset_id/delete</request_matched_uri>
    <request_result>success</request_result>
    <request_result_description></request_result_description>
    <request_result_id>601</request_result_id>
    <request_uri>[POST] asset/u1mkXRZCcJNEaXb5JMtqrH3K/delete</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.057</request_process_time>
  </header>
  <items/>
</response>
```

#### Example implementation

**.NET Core:**

```csharp
public async Task<JObject> DeleteAsset(string assetId, string userId)
{
    var requestContent = new MultipartFormDataContent();

    requestContent.Add(new StringContent(userId), "user_id");
    requestContent.Add(new StringContent("cascade"), "delete");

    var response = await _httpClient.PostAsync($"asset/{assetId}/delete", requestContent);

    return await ParseResult(response, "DeleteAsset");
}
```

**Node.js:**

```js
async delete(assetId, userId) {
    const formData = {
        user_id: userId,
        delete: "cascade"
    };

    const response = await this.requestClient.post(`asset/${assetId}/delete`, { formData });

    return await this.parseResult(response, "Delete");
}
```
