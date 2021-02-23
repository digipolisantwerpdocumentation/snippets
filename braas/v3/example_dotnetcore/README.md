# BRaaS API example app (.NET Core)

## Prerequisites

- .NET Core 3.1 SDK or Docker to build and run the application.
- API-key of an application which has a contract with the BRaaS v3 API in ACC.
- Optionally a JWT token of a user or application which is known in BRaaS. If no JWT token is provided, the application of the API key should be known in BRaaS (as described on the [ACPaaS Portal](https://acpaas.digipolis.be/nl/product/braas-engine/v1.0.0/features#applicatiesubjecten)).

## Start example

First add your API key and (optionally) JWT to [Config.cs](Config.cs).

```
dotnet run
```

Or using Docker:

```
docker build --tag braas_example_dotnetcore .

docker run --interactive --tty braas_example_dotnetcore
```
