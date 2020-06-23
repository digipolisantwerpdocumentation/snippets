# Digital Vault API example app (.NET Core)

## Prerequisites

- .NET Core 3.1 SDK or Docker to build and run the application.
- OAuth client-id en client-secret key of an application which has a contract with the Digital Vault 2.0 API in ACC.

## Start example

```
dotnet run
```

Or using Docker:

```
docker build --tag digitalvault_example_dotnetcore .

docker run --interactive --tty digitalvault_example_dotnetcore
```

You can change the default base URL and OAuth config-keys in [Config.cs](Config.cs).

## Example output

```
Starting Digital Vault example app

POST check document exists response (200): {"success":true,"exist":false}
POST upload document response (201): {"success":true,"id":47221505,"md5":"vZeAR3YU2BrM4DkCD0WzVg=="}

```