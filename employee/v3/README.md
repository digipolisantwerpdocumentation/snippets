# Employee v3 examples (.NET Core)

The Employee v3 API provides an API for retrieving employee data.

## Table of contents

<!--
Regenerate table of contents with:

npm install --global markdown-toc
markdown-toc -i --maxdepth 3 employee/v3/README.md
-->

<!-- toc -->

- [Links](#links)
- [Example apps](#example-apps)
- [Code snippets](#code-snippets)
  * [Setup](#setup)
  * [GET employee](#get-employee)
  * [Search employees](#search-employees)

<!-- tocstop -->

## Links

- [Employee engine on ACPaaS Portal](https://acpaas.digipolis.be/nl/product/crs-medewerker)
- [API Store ACC](https://api-store-a.antwerpen.be/#/org/acpaas/api/employee/v3/documentation)
- [ACPaaS Wiki page](https://wiki.antwerpen.be/ACPAAS/index.php/CRS_Medewerker)

## Example apps

- [.NET Core](example_dotnetcore/README.md)
- [Node.js](example_nodejs/README.md)

## Code snippets

These snippets and other examples are available in [EmployeeService.cs (.NET Core)](example_dotnetcore/ServiceAgent/EmployeeService.cs) 
and [employeeService.js (Node.js)](example_nodejs/EmployeeService.js).

### Setup

First configure the base URL, API-key and OAUTH2 keys in Config.cs:

**.NET Core:**

```csharp
public static class Config
    {
		public static string ApiKey = "<YOUR-API-KEY>";
        public static string OAuthClientId = "<YOUR-OAUTH-CLIENTID>";
        public static string OAuthClientSecret = "<YOUR-OAUTH-CLIENTSECRET>";

        public static string BaseAddress = "https://api-gw-a.antwerpen.be/acpaas/employee/v3/";
    }
```

**Node.js:**

create .env file from template example.env
start script

* `npm run sync`
* `npm run async`


### GET employee

Retrieve employee by its reference number.

Request:

`GET /employees/{crsNumber}`

Response (status code 200 ok):

```json
{
	"LastName": "Dummy",
	"FirstName": "test",
	"NickName": "test",
	"AvatarUrl": null,
	"CrsNumber": 100,
	"Source": {
		"Code": "SAP",
		"Description": "SAP",
		"Key": "64000100",
		"ReferenceKey": "123456"
	},
	"Identity": {
		"NationalNumber": "20020201022",
		"PersonCrsNumber": "00123456"
	},
	"AdAccount": {
		"Domain": "<DOMAIN>",
		"Account": "<DOMAINACCOUNT>"
	},
	"Organisation": {
		"OrganisationCrsNumber": 0,
		"Description": null
	}
}
```

Example implementation:
- .net core: see method GetEmployee in [EmployeeService.cs](example_dotnetcore/ServiceAgent/EmployeeService.cs).


### Search employees

Search employees by a number of search criteria. The result is a paged HAL-response.
This request uses only a subset of the possible query parameters. See swagger-documentation for all possible options.
There are 2 possible paging strategies: withcount and nocount. When nocount is used, the totalPages- and totalElements-properties are always 0.

Request :

`GET /employees?Page=1&PageSize=10&PagingStrategy=withcount&Includes=organisation&NameContains=dummy&IsSupervisor=False


Response (status code 200 ok):

```json
{
	"_links": {
		"Self": {
			"Href": "http://api-gw-a.antwerpen.be/acpaas/employee/v3/employees?page=1&pagesize=10&includes=organisation&namecontains=dummy&issupervisor=false&paging-strategy=withcount&sort=id"
		},
		"First": {
			"Href": "http://api-gw-a.antwerpen.be/acpaas/employee/v3/employees?page=1&pagesize=10&includes=organisation&namecontains=dummy&issupervisor=false&paging-strategy=withcount&sort=id"
		},
		"Last": {
			"Href": "http://api-gw-a.antwerpen.be/acpaas/employee/v3/employees?page=48&pagesize=10&includes=organisation&namecontains=dummy&issupervisor=false&paging-strategy=withcount&sort=id"
		},
		"Next": {
			"Href": "http://api-gw-a.antwerpen.be/acpaas/employee/v3/employees?page=2&pagesize=10&includes=organisation&namecontains=dummy&issupervisor=false&paging-strategy=withcount&sort=id"
		},
		"Previous": null
	},
	"_embedded": {
		"employees": [
			{
				"LastName": "Dummy",
				"FirstName": "test",
				"NickName": "test",
				"AvatarUrl": null,
				"CrsNumber": 100,
				"Source": {
					"Code": "SAP",
					"Description": "SAP",
					"Key": "64000100",
					"ReferenceKey": "123456"
				},
				"Identity": {
					"NationalNumber": "20020201022",
					"PersonCrsNumber": "00123456"
				},
				"AdAccount": {
					"Domain": "<DOMAIN>",
					"Account": "<DOMAINACCOUNT>"
				},
				"Organisation": {
					"OrganisationCrsNumber": "1234",
					"Description": "TESTORG"
				}
			},
			{
			...
			}
			, ...
		]
	},
	"_page": {
		"number": 1,
		"size": 10,
		"totalElements": 115,
		"totalPages": 12
	}
}
```

Example implementation:
- .net core: see method SearchEmployees in [EmployeeService.cs](example_dotnetcore/ServiceAgent/EmployeeService.cs).
