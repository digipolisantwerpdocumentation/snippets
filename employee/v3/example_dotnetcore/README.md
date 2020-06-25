# Employee API example app (.NET Core)

## Prerequisites

- .NET Core 3.1 SDK or Docker to build and run the application.
- API-key, OAuth client-id and client-secret key of an application which has a contract with the Employee v3 API in ACC.

## Start example

```
dotnet run
```

Or using Docker:

```
docker build --tag employee_example_dotnetcore .

docker run --interactive --tty employee_example_dotnetcore
```

You can change the default base URL and other config-keys in [Config.cs](Config.cs).

## Example output

```
Starting Employee example app

GET employee response (200): {"LastName":"Dummy","FirstName":"test","NickName":"test","AvatarUrl":null,"CrsNumber":100,"Source":{"Code":"SAP","Description":"SAP","Key":"64000100","ReferenceKey":"123456"},"Identity":{"NationalNumber":"20020201022","PersonCrsNumber":"00123456"},"AdAccount":{"Domain":"DOMAIN","Account":"DOMAINACCOUNT"},"Organisation":{"OrganisationCrsNumber":0,"Description":null}}
GET employees response (200): {"_links":{"Self":{"Href":"http://api-gw-a.antwerpen.be/acpaas/employee/v3/employees?page=1\u0026pagesize=10\u0026includes=organisation\u0026namecontains=dummy\u0026issupervisor=false\u0026paging-strategy=withcount\u0026sort=id"},"First":{"Href":"http://api-gw-a.antwerpen.be/acpaas/employee/v3/employees?page=1\u0026pagesize=10\u0026includes=organisation\u0026namecontains=dummy\u0026issupervisor=false\u0026paging-strategy=withcount\u0026sort=id"},"Last":{"Href":"http://api-gw-a.antwerpen.be/acpaas/employee/v3/employees?page=48\u0026pagesize=10\u0026includes=organisation\u0026namecontains=dummy\u0026issupervisor=false\u0026paging-strategy=withcount\u0026sort=id"},"Next":{"Href":"http://api-gw-a.antwerpen.be/acpaas/employee/v3/employees?page=2\u0026pagesize=10\u0026includes=organisation\u0026namecontains=dummy\u0026issupervisor=false\u0026paging-strategy=withcount\u0026sort=id"},"Previous":null},"_embedded":{"Employees":[{"LastName":"Dummy","FirstName":"test","NickName":"test","AvatarUrl":null,"CrsNumber":100,"Source":{"Code":"SAP","Description":"SAP","Key":"64000100","ReferenceKey":"123456"},"Identity":{"NationalNumber":"20020201022","PersonCrsNumber":"00123456"},"AdAccount":{"Domain":"DOMAIN","Account":"DOMAINACCOUNT"},"Organisation":{"OrganisationCrsNumber":0,"Description":null}}, {...}, ...]},"_page":{"Number":1,"Size":10,"TotalElements":474,"TotalPages":48}}

```