# Output Generator examples (.NET Core)

The Output Generator API provides a API to generate a variety of document files based on templates and data/content files.
Possible appliances are forms, personalized documents, letters, mails, ...

These code snippets and console app show how to generate a PDF from a Word-template file and download the document. This
can be done synchronously and asynchronously.

## Table of contents

<!--
Regenerate table of contents with:

npm install --global markdown-toc
markdown-toc -i --maxdepth 3 outputgenerator/v6/README.md
-->

<!-- toc -->

- [Links](#links)
- [Example apps](#example-apps)
- [Code snippets](#code-snippets)
  * [Setup](#setup)
  * [Generate PDF from Word-template (docx) SYNCHRONOUS](#generate-pdf-from-word-template-docx-synchronous)
  * [Generate PDF from Word-template (docx) ASYNCHRONOUS](#generate-pdf-from-word-template-docx-asynchronous)

<!-- tocstop -->

## Links

- [Ouput Generator engine on ACPaaS Portal](https://acpaas.digipolis.be/nl/product/output-generator-engine)
- [API Store ACC](https://api-store-a.antwerpen.be/#/org/acpaas/api/outputgenerator/v6/documentation)
- [ACPaaS Wiki page](https://wiki.antwerpen.be/ACPAAS/index.php/Output_Generator_Engine)

## Example apps

- [.NET Core](example_dotnetcore/README.md)
- [Node.js](example_nodejs/README.md)

## Code snippets

These snippets and other examples are available in [OutputGeneratorService.cs (.NET Core)](example_dotnetcore/OutputGeneratorService.cs) 
and [outputGeneratorService.js (Node.js)](example_nodejs/outputGeneratorService.js).

### Setup

First configure the base URL and API key in Config.cs:

**.NET Core:**

```csharp
public static class Config
    {
        public static string ApiKey = "<YOUR-API-KEY>";

        public static string BaseAddress = "https://api-gw-a.antwerpen.be/acpaas/outputgenerator/v6/";
    }
```

**Node.js:**

```js
```

### Generate PDF from Word-template (docx) SYNCHRONOUS

The document is generated synchronously. A download link is provided as part of the generation result.
Be aware for possible time outs or unwanted delays when you want to generate large documents with complex structures 
or when the output generator experiences a high amount of simultaneous requests. To avoid al these problems, 
async document creation is preferred.

Generate document with async=false

`POST /generator/directWordGeneration`

Response:

```json
{
	"messages": [],
	"success": true,
	"value": {
		"cephId": null,
		"creationDate": "2020-03-23T10:33:01",
		"creator": {
			"className": "User",
			"classPrefix": "user",
			"componentName": "account",
			"dataRefLink": "user-detail?userOid=1453663",
			"id": 1453663,
			"name": "int-myorganisation.testoutputgenerator.v1",
			"packName": "net.democritus.usr"
		},
		"dataRef": {
			"className": "",
			"classPrefix": "",
			"componentName": "",
			"dataRefLink": "",
			"id": 1674789,
			"name": "export_2020.03.23_10.33.01.420_5851bb5b-3f2c-4183-a359-b304dd1ab834.pdf",
			"packName": ""
		},
		"elementName": "File",
		"elementPackage": "org.normalizedsystems.file",
		"id": 1674789,
		"name": "export_2020.03.23_10.33.01.420_5851bb5b-3f2c-4183-a359-b304dd1ab834.pdf",
		"status": null,
		"statusAsEnum": "NOT_MAPPED",
		"uploadUri": "generated/export_2020.03.23_10.33.01.420_5851bb5b-3f2c-4183-a359-b304dd1ab834.pdf"
	}
}
```

Download document
`POST /download?name=generated/export_2020.03.23_10.33.01.420_5851bb5b-3f2c-4183-a359-b304dd1ab834.pdf`

Response: byte stream of document file

Example implementation:
- .net core: see method GeneratePDFFromWordTemplate in [OutputGeneratorService.cs](example_dotnetcore/OutputGeneratorService.cs).


### Generate PDF from Word-template (docx) ASYNCHRONOUS

The document is generated asynchronously. A link for checking the generation status is provided as part of the generation result.

Generate document with async=true

`POST /generator/directWordGeneration`

Response (status code 202 accepted):

```json
"/digipolis/generator/task/result/d67e8e15-77cb-4d18-9b14-d29a89023431"
```

Check document status

Depending on the document status a different response and status code may be returned. If the document is ready,
a 303-redirect response is provided. Be aware to disable automatic redirect! The link in the location-header doesn't provide
a valid link to the document for the moment. The redirect/download of the document is done manually in this code snippet.

The document check and download can be done in a separate background process to benefit from this async call 
and as for not blocking the initial request. Otherwise this is still a synchronous call from the perspective 
of the requester of your API.

`GET /generator/task/result/d67e8e15-77cb-4d18-9b14-d29a89023431`

Response when document is generated and ready for download (status code 303):

```json
{
	"cephId": null,
	"creationDate": "2020-03-20T14:04:56",
	"creator": {
		"className": "User",
		"classPrefix": "user",
		"componentName": "account",
		"dataRefLink": "user-detail?userOid=1453663",
		"id": 1453663,
		"name": "int-myorganisation.testoutputgenerator.v1",
		"packName": "net.democritus.usr"
	},
	"dataRef": {
		"className": "",
		"classPrefix": "",
		"componentName": "",
		"dataRefLink": "",
		"id": 1661928,
		"name": "export_2020.03.20_14.04.56.351_d67e8e15-77cb-4d18-9b14-d29a89023431.pdf",
		"packName": ""
	},
	"elementName": "File",
	"elementPackage": "org.normalizedsystems.file",
	"id": 1661928,
	"name": "export_2020.03.20_14.04.56.351_d67e8e15-77cb-4d18-9b14-d29a89023431.pdf",
	"status": null,
	"statusAsEnum": "NOT_MAPPED",
	"uploadUri": "generated/export_2020.03.20_14.04.56.351_d67e8e15-77cb-4d18-9b14-d29a89023431.pdf"
}
```

Response when generation failed (status code 200):

```json
{
	"status": "failed"
}
```

Download document

`POST /download?name=generated/export_2020.03.20_14.04.56.351_d67e8e15-77cb-4d18-9b14-d29a89023431.pdf`

Response: byte stream of document file

Example implementation:
- .net core: see method GenerateAsyncPDFFromWordTemplate in [OutputGeneratorService.cs](example_dotnetcore/OutputGeneratorService.cs).
