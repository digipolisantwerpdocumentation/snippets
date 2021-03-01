# BRaaS API examples

These examples show how to get data from the BRaaS API.

The BRaaS API offers full CRUD functionalities which are not covered by these examples.

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

- [BRaaS engine on ACPaaS Portal](https://acpaas.digipolis.be/nl/product/braas-engine)
- [API Store ACC](https://api-store-a.antwerpen.be/#/org/acpaas/api/braas/v3/documentation)
- [ACPaaS Wiki page](https://wiki.antwerpen.be/ACPAAS/index.php/Business_Roles_engine_(BraaS))

## Example apps

- [.NET Core](example_dotnetcore)
- [Node.js](example_nodejs)

## Code snippets

These snippets are available in [BraasService.cs (.NET Core)](example_dotnetcore/BraasService.cs) and [braasService.js (Node.js)](example_nodejs/braasService.js).

### Setup

First configure the base URL and API key:

**.NET Core:**

```csharp
private readonly HttpClient _httpClient;

public BraasService(string baseAddress, string apiKey)
{
    // Use IHttpClientFactory (AddHttpClient) in real implementations
    _httpClient = new HttpClient();
    _httpClient.BaseAddress = new Uri(baseAddress);
    _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
}

private Task<HttpResponseMessage> GetAsync(string uri, string jwt)
{
    using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri))
    {
        if (jwt != null)
        {
            requestMessage.Headers.Add("Dgp-Authorization-For", $"Bearer {jwt}");
        }

        return _httpClient.SendAsync(requestMessage);
    }
}
```

**Node.js:**

```js
class BraasService {
    constructor(config) {
        this.axiosInstance = axios.create({
            baseURL: config.baseAddress,
            headers: {
                ApiKey: config.apiKey,
            },
        });
    }

    getOptions(jwt) {
        const options = {
            headers: {},
        };

        if (jwt) {
            options.headers["Dgp-Authorization-For"] = `Bearer ${jwt}`;
        }

        return options;
    }
}
```

### Get application

`GET https://api-gw-a.antwerpen.be/acpaas/braas/v3/applications/{applicationId}`

Response:

```json
{
  "application": {
    "attributes": {
      "CRUD": "",
      "DOMEIN": null,
      "LOCATIE": null,
      "STADSBEDRIJF": "zwijndrecht"
    },
    "description": null,
    "displayName": "testapprc01704-someTenantKey",
    "id": "97df3524-3675-4240-9ffc-36f2a1bd99b6",
    "name": "testapprc01704-someTenantKey",
    "targetApplicationUniqueKey": "testapprc01704",
    "tenantKey": "someTenantKey"
  }
}
```

#### Example implementation

**.NET Core:**

```csharp
public async Task<JObject> GetApplication(string applicationId, string jwt)
{
    var responseMessage = await GetAsync($"applications/{applicationId}", jwt);
    var responseContent = await responseMessage.Content.ReadAsStringAsync();

    if (!responseMessage.IsSuccessStatusCode)
    {
        throw new Exception($"GetApplication failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"GetApplication response ({(int)responseMessage.StatusCode}): {responseContent}");
    // GetApplication response (200): {"application":{"attributes":{"CRUD":"","DOMEIN":null,"LOCATIE":null,"STADSBEDRIJF":"zwijndrecht"},"description":null,"displayName":"testapprc01704-someTenantKey","id":"97df3524-3675-4240-9ffc-36f2a1bd99b6","name":"testapprc01704-someTenantKey","targetApplicationUniqueKey":"testapprc01704","tenantKey":"someTenantKey"}}

    return JObject.Parse(responseContent);
}
```

**Node.js:**

```js
async getApplication(applicationId, jwt) {
    const response = await this.axiosInstance.get(`applications/${applicationId}`, this.getOptions(jwt));
    console.log(`getApplication response (${response.status}): ${JSON.stringify(response.data)}`);
    // getApplication response (200): {"application":{"attributes":{"CRUD":"","DOMEIN":null,"LOCATIE":null,"STADSBEDRIJF":"zwijndrecht"},"description":null,"displayName":"testapprc01704-someTenantKey","id":"97df3524-3675-4240-9ffc-36f2a1bd99b6","name":"testapprc01704-someTenantKey","targetApplicationUniqueKey":"testapprc01704","tenantKey":"someTenantKey"}}

    return await response.data;
}
```

### Get application roles

`GET https://api-gw-a.antwerpen.be/acpaas/braas/v3/applications/{applicationId}/roles`

Response:

```json
{
  "cursor": 126,
  "roles": [
    {
      "attributes": {
        "BEHEERDER": "",
        "CRUD": "",
        "DOMEIN": null,
        "LOCATIE": null,
        "STADSBEDRIJF": null
      },
      "description": null,
      "id": "08364e92-6116-4b1d-8da9-a565d89ad148",
      "name": "Test role rc01704",
      "validFrom": null,
      "validTo": null
    }
  ]
}
```

#### Example implementation

**.NET Core:**

```csharp
public async Task<JObject> GetApplicationRoles(string applicationId, string jwt)
{
    var responseMessage = await GetAsync($"applications/{applicationId}/roles", jwt);
    var responseContent = await responseMessage.Content.ReadAsStringAsync();

    if (!responseMessage.IsSuccessStatusCode)
    {
        throw new Exception($"GetApplicationRoles failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"GetApplicationRoles response ({(int)responseMessage.StatusCode}): {responseContent}");
    // GetApplicationRoles response (200): {"cursor":126,"roles":[{"attributes":{"BEHEERDER":"","CRUD":"","DOMEIN":null,"LOCATIE":null,"STADSBEDRIJF":null},"description":null,"id":"08364e92-6116-4b1d-8da9-a565d89ad148","name":"Test role rc01704","validFrom":null,"validTo":null}]}

    return JObject.Parse(responseContent);
}
```

**Node.js:**

```js
async getApplicationRoles(applicationId, jwt) {
    const response = await this.axiosInstance.get(`applications/${applicationId}/roles`, this.getOptions(jwt));
    console.log(`getApplicationRoles response (${response.status}): ${JSON.stringify(response.data)}`);
    // getApplicationRoles response (200): {"cursor":126,"roles":[{"attributes":{"BEHEERDER":"","CRUD":"","DOMEIN":null,"LOCATIE":null,"STADSBEDRIJF":null},"description":null,"id":"08364e92-6116-4b1d-8da9-a565d89ad148","name":"Test role rc01704","validFrom":null,"validTo":null}]}

    return await response.data;
}
```

### Get role teams

`GET https://api-gw-a.antwerpen.be/acpaas/braas/v3/roles/{roleId}/teams`

Response:

```json
{
  "cursor": 1557930635498103,
  "teams": [
    {
      "attributes": {
        "CRUD": null,
        "DOMEIN": null,
        "LOCATIE": "test1",
        "STADSBEDRIJF": null
      },
      "description": null,
      "id": "7a1bc4e8-1417-42e6-90ec-e646b9188d00",
      "joinApprovalRequired": false,
      "name": "Test team rc01704",
      "open": true,
      "validFrom": null,
      "validTo": null,
      "visible": true
    }
  ]
}
```

#### Example implementation

**.NET Core:**

```csharp
public async Task<JObject> GetRoleTeams(string roleId, string jwt)
{
    var responseMessage = await GetAsync($"roles/{roleId}/teams", jwt);
    var responseContent = await responseMessage.Content.ReadAsStringAsync();

    if (!responseMessage.IsSuccessStatusCode)
    {
        throw new Exception($"GetRoleTeams failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"GetRoleTeams response ({(int)responseMessage.StatusCode}): {responseContent}");
    // GetRoleTeams response (200): {"cursor":1557930635498103,"teams":[{"attributes":{"CRUD":null,"DOMEIN":null,"LOCATIE":"test1","STADSBEDRIJF":null},"description":null,"id":"7a1bc4e8-1417-42e6-90ec-e646b9188d00","joinApprovalRequired":false,"name":"Test team rc01704","open":true,"validFrom":null,"validTo":null,"visible":true}]}

    return JObject.Parse(responseContent);
}
```

**Node.js:**

```js
async getRoleTeams(roleId, jwt) {
    const response = await this.axiosInstance.get(`roles/${roleId}/teams`, this.getOptions(jwt));
    console.log(`getRoleTeams response (${response.status}): ${JSON.stringify(response.data)}`);
    // getRoleTeams response (200): {"cursor":1557930635498103,"teams":[{"attributes":{"CRUD":null,"DOMEIN":null,"LOCATIE":"test1","STADSBEDRIJF":null},"description":null,"id":"7a1bc4e8-1417-42e6-90ec-e646b9188d00","joinApprovalRequired":false,"name":"Test team rc01704","open":true,"validFrom":null,"validTo":null,"visible":true}]}

    return await response.data;
}
```

### Get team subjects

`GET https://api-gw-a.antwerpen.be/acpaas/braas/v3/teams/{teamId}/subjects`

Response:

```json
{
  "cursor": 110668,
  "members": [
    {
      "address": null,
      "domain": "ICA",
      "email": "Some.User@digipolis.be",
      "externalMutableReference": "rc01704@digant.antwerpen.local",
      "firstname": "Some",
      "id": "3aebe845-0a71-443a-b868-5c4f0e1b84cc",
      "lastname": "User",
      "nickname": null,
      "owner": true,
      "type": "mprofiel",
      "username": "rc01704"
    },
    {
      "address": null,
      "domain": "ICA",
      "email": "Other.User@digipolis.be",
      "externalMutableReference": "rc00992@digant.antwerpen.local",
      "firstname": "Other",
      "id": "29730b11-7056-48c3-80ef-b6de3904455c",
      "lastname": "User",
      "nickname": null,
      "owner": false,
      "type": "mprofiel",
      "username": "rc00992"
    }
  ]
}
```

#### Example implementation

**.NET Core:**

```csharp
public async Task<JObject> GetTeamSubjects(string teamId, string jwt)
{
    var responseMessage = await GetAsync($"teams/{teamId}/subjects", jwt);
    var responseContent = await responseMessage.Content.ReadAsStringAsync();

    if (!responseMessage.IsSuccessStatusCode)
    {
        throw new Exception($"GetTeamSubjects failed ({(int)responseMessage.StatusCode}): {responseContent}");
    }

    Console.WriteLine($"GetTeamSubjects response ({(int)responseMessage.StatusCode}): {responseContent}");
    // GetTeamSubjects response (200): {"cursor":110668,"members":[{"address":null,"domain":"ICA","email":"Some.User@digipolis.be","externalMutableReference":"rc01704@digant.antwerpen.local","firstname":"Some","id":"3aebe845-0a71-443a-b868-5c4f0e1b84cc","lastname":"User","nickname":null,"owner":true,"type":"mprofiel","username":"rc01704"},{"address":null,"domain":"ICA","email":"Other.User@digipolis.be","externalMutableReference":"rc00992@digant.antwerpen.local","firstname":"Other","id":"29730b11-7056-48c3-80ef-b6de3904455c","lastname":"User","nickname":null,"owner":false,"type":"mprofiel","username":"rc00992"}]}

    return JObject.Parse(responseContent);
}
```

**Node.js:**

```js
async getTeamSubjects(teamId, jwt) {
    const response = await this.axiosInstance.get(`teams/${teamId}/subjects`, this.getOptions(jwt));
    console.log(`getTeamSubjects response (${response.status}): ${JSON.stringify(response.data)}`);
    // getTeamSubjects response (200): {"cursor":110668,"members":[{"address":null,"domain":"ICA","email":"Some.User@digipolis.be","externalMutableReference":"rc01704@digant.antwerpen.local","firstname":"Some","id":"3aebe845-0a71-443a-b868-5c4f0e1b84cc","lastname":"User","nickname":null,"owner":true,"type":"mprofiel","username":"rc01704"},{"address":null,"domain":"ICA","email":"Other.User@digipolis.be","externalMutableReference":"rc00992@digant.antwerpen.local","firstname":"Other","id":"29730b11-7056-48c3-80ef-b6de3904455c","lastname":"User","nickname":null,"owner":false,"type":"mprofiel","username":"rc00992"}]}

    return await response.data;
}
```
