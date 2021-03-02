# WCM Content Manager API example app (.NET Core)

## Prerequisites

- .NET Core 3.1 SDK or Docker to build and run the application.
- API key of an application which has a contract with the WCM Content Manager v3 API in ACC.
- Name of your WCM tenant.

## Start example

First add your config to [Config.cs](Config.cs).

```
dotnet run
```

Or using Docker:

```
docker build --tag wcm_example_dotnetcore .

docker run --interactive --tty wcm_example_dotnetcore
```
