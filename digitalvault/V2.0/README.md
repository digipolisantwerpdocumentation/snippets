# Digital Vault 2.0 examples (.NET Core)

The Digital Vault 2.0 API (a.k.a. Docbox) provides a API for document providers to store sensitive or personal files to personal vaults.
Documents are organised and uploaded within organisation units. This is important for the administrative side
of the Digital Vault. Document providers (uploaders) or administrators (view uploaded documents, commit, ...) can
have security rights for one of more organisations (with all organisation units belonging to it) or for one or more organisation units at the lowest level.

For an end-user this is mere semantical. A user sees all its documents in his vault as a single list. The organisation units are
shown as possible search filters and are a indication of the document's origin.

An uploaded document must have a document type. A document type defines the actual file type (PDF, JPG, PNG), but also
a list of possible metadata values. A document type must be defined in Digital Vault within an organisation unit
before a document can be uploaded (in the near future the admin UI will provide the necessary tools). The same document type can be used
for multiple kinds of documents (payslip, formal letter from HR, ...) as long as the mime-type and metadata stays the same.

Documents also have a bulk operation property. Documents are uploaded one by one and providing the same bulk operation name 
with each upload allows the Digital Vault to group documents together within an organisation unit.

A document can be uploaded to one ore more destinations (= vaults) at the same time.

These code snippets and console app show how to upload a file and check if a file is already uploaded.

## Table of contents

<!--
Regenerate table of contents with:

npm install --global markdown-toc
markdown-toc -i --maxdepth 3 digitalvault/v2.0/README.md
-->

<!-- toc -->

- [Links](#links)
- [Example apps](#example-apps)
- [Code snippets](#code-snippets)
  * [Setup](#setup)
  * [Check if a document is already uploaded](#check-if-a-document-is-already-uploaded)
  * [Upload a document to a single destination](#upload-a-document-to-a-single-destination)

<!-- tocstop -->

## Links

- [Digital Vault engine on ACPaaS Portal](https://acpaas.digipolis.be/nl/product/docbox)
- [API Store ACC](https://api-store-a.antwerpen.be/#/org/acpaas/api/vault/v1/documentation)
- [ACPaaS Wiki page](https://wiki.antwerpen.be/ACPAAS/index.php/Digital_Vault_v2)

## Example apps

- [.NET Core](example_dotnetcore/README.md)
- [Node.js](example_nodejs/README.md)

## Code snippets

These snippets and other examples are available in [DigitalVaultService.cs (.NET Core)](example_dotnetcore/DigitalVaultService.cs) 
and [digitalVaultService.js (Node.js)](example_nodejs/digitalVaultService.js).

### Setup

First configure the base URL and OAUTH2 keys in Config.cs:

**.NET Core:**

```csharp
public static class Config
    {
        public static string OAuthClientId = "<YOUR-OAUTH-CLIENTID>";
        public static string OAuthClientSecret = "<YOUR-OAUTH-CLIENTSECRET>";

        public static string BaseAddress = "https://api-gw-a.antwerpen.be/acpaas/digitalvault/v1/";
    }
```

**Node.js:**

create .env file from template example.env
start script

* `npm run sync`
* `npm run async`


### Check if a document is already uploaded

In some situations it can be helpful to check if a document is already uploaded. By providing a document name, reference date, 
bulk operation name and a destination you get a simple response with a boolean property: exists or not exists.
A check is best performed for a single destination.


`POST /organizationunits/{organizationUnitId}/documents/check`

Request:

```json
{
	"name": "testdocument.pdf",
	"referenceDate": "2020-06-23T00:00:00",
	"destinations": [
		"2020010112345"
	],
	"bulkOperation": "bulkupload_test_20200623"
}
```

Response (status code 200 ok):

```json
{
	"success": true,
	"exist": false
}
```

Example implementation:
- .net core: see method CheckDocumentExists in [DigitalVaultService.cs](example_dotnetcore/DigitalVaultService.cs).


### Upload a document to a single destination

This example shows how to upload a single document by a document provider to a user's vault.
The upload is done within the confines of an organisation unit.

Metadata properties can be defined as required or not required for a document type. If required, they must be provided during an upload.
A metadata property can't have an empty value; required or not. This results in an 500-error during upload.

`POST /organizationunits/{organizationUnitId}/documents`

Request :

```json
{
	"name": "testdocument.pdf",
	"externalName": "My testdocument of 23/06/2020",
	"content": "<base64-encoded byte array>",
	"documentType": "normal",
	"referenceDate": "2020-06-23T00:00:00",
	"bulkOperation": "bulkupload_test_20200623",
	"destinations": [
		{
			"name": "2020010112345",
			"autoCommit": true,
			"tags": [
				"test",
				"2020010112345"
			],
			"notificationNeeded": false
		}
	],
	"metadata": [
		{
			"key": "user_id",
			"value": "123456"
		},
		{
			"key": "tags",
			"value": "001234"
		},
		{
			"key": "documentname",
			"value": "My testdocument of 23/06/2020"
		},
		{
			"key": "ref_month",
			"value": "2020/06"
		},
		{
			"key": "name",
			"value": "2020010112345"
		},
		{
			"key": "firstname",
			"value": "test"
		},
		{
			"key": "nationalnumber",
			"value": "2020010112345"
		},
		{
			"key": "person-number",
			"value": "001234"
		},
		{
			"key": "location",
			"value": "location 1"
		},
		{
			"key": "unit",
			"value": "snip-it"
		},
		{
			"key": "employer",
			"value": "Snippet Inc."
		},
		{
			"key": "filename",
			"value": "testdocument.pdf"
		}
	]
}
```

Response (status code 201 created):

```json
{
	"success": true,
	"id": 47221530,
	"md5": "vZeAR3YU2BrM4DkCD0WzVg=="
}
```

Example implementation:
- .net core: see method UploadWithRetry in [DigitalVaultService.cs](example_dotnetcore/DigitalVaultService.cs).
