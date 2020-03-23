# Output Generator API example app (.NET Core)

## Prerequisites

- .NET Core 3.0 SDK or Docker to build and run the application.
- API key of an application which has a contract with the Output Generator API in ACC.

## Start example

```
dotnet run
```

Or using Docker:

```
docker build --tag output-generator_example_dotnetcore .

docker run --interactive --tty output-generator_example_dotnetcore
```

You can change the default base URL and API key in [Config.cs](Config.cs).

## Example output

```
Starting Output Generator example app
Enter your API key (<YOUR-API-KEY>): <some API key>
Using API key "<some API key>"

POST generate document response (200): {"messages":[],"success":true,"value":{"cephId":null,"creationDate":"2020-03-23T12:01:14","creator":{"className":"User","classPrefix":"user","componentName":"account","dataRefLink":"user-detail?userOid=1453663","id":1453663,"name":"int-myorganisation.testoutputgenerator.v1","packName":"net.democritus.usr"},"dataRef":{"className":"","classPrefix":"","componentName":"","dataRefLink":"","id":1674822,"name":"export_2020.03.23_12.01.14.174_62475cc9-fdfa-4182-a038-1718c638b26d.pdf","packName":""},"elementName":"File","elementPackage":"org.normalizedsystems.file","id":1674822,"name":"export_2020.03.23_12.01.14.174_62475cc9-fdfa-4182-a038-1718c638b26d.pdf","status":null,"statusAsEnum":"NOT_MAPPED","uploadUri":"generated\/export_2020.03.23_12.01.14.174_62475cc9-fdfa-4182-a038-1718c638b26d.pdf"}}
POST async generate document response (202): "\/digipolis\/generator\/task\/result\/f7a498ff-118e-48e2-ba0e-0ab22ce7ff84"
GET document status response (303): {"cephId":null,"creationDate":"2020-03-20T14:04:56","creator":{"className":"User","classPrefix":"user","componentName":"account","dataRefLink":"user-detail?userOid=1453663","id":1453663,"name":"int-myorganisation.testoutputgenerator.v1","packName":"net.democritus.usr"},"dataRef":{"className":"","classPrefix":"","componentName":"","dataRefLink":"","id":1661928,"name":"export_2020.03.20_14.04.56.351_d67e8e15-77cb-4d18-9b14-d29a89023431.pdf","packName":""},"elementName":"File","elementPackage":"org.normalizedsystems.file","id":1661928,"name":"export_2020.03.20_14.04.56.351_d67e8e15-77cb-4d18-9b14-d29a89023431.pdf","status":null,"statusAsEnum":"NOT_MAPPED","uploadUri":"generated\/export_2020.03.20_14.04.56.351_d67e8e15-77cb-4d18-9b14-d29a89023431.pdf"}
...
```