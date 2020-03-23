![LoggingEngine](../assets/ApplicationLogging.png)

# LoggingEngine

### Table of contents:

* [General Info](https://acpaas.digipolis.be/nl/product/logging-engine)
* [User Manual](https://wiki.antwerpen.be/ACPAAS/index.php/Logging_Engine)
* V1 - [Structure Logging Message](#StructureMessageLogging) 
* V1 - ApplicationLogging
 	* [Swagger](https://api-store-o.antwerpen.be/#/org/acpaas/api/applicationlogging/v1/documentation)
 	* code examples
 		* [C#](#V1ApplicationLoggingCodeExampleCSharp) 
 		* [NodeJs](#V1ApplicationLoggingCodeExampleNodeJs)
 * V1 - SystemLogging
 	* [Swagger](https://api-store-o.antwerpen.be/#/org/acpaas/api/systemlogging/v1/documentation) 
 	* code examples
 		* [C#](#V1SystemLoggingCodeExampleCSharp) 
 		* [NodeJs](#V1SystemLoggingCodeExampleNodeJs)

***
* <a name="StructureMessageLogging"/>V1 - Structure Message Logging

|Fieldname|  |  | type |  required| example and explanation |
------|------|------|-----|------|-----|
|Header|  |  |  |  |  |
| Index |  |  | string | required | index-app-invoices |
| Timestamp |  |  | datetime | required | 2012-01-01T12:00:00Z |
| Version |  |  | string | required | 1 <br /> version of loggingengine |
| Correlation |  |  |  |  |  |
|  | CorrelationId |  | string | required | 95f419b1-a11f-474b-92bb-dd17fb9d70ef <br /> unique identifier of request |
|  | Application |  |  |  |  |
|  |  | ApplicationId | string | required | ac59caba-724d-4005-ac76-6ac5f1e10a9c <br /> unique identifier of application starting the request |
|  |  | ApplicationName | string | required | PaymentApp<br />display-friendly name of requesting application|
|  | Instance |  |  |  |  |
|  |  | InstanceId | string | required | 0062cdae-3d86-4d1b-9309-44ae8d209a92<br />unique identifier of application-instance starting the request |
|  |  | InstanceName | string | required | PaymentApp-instance-1<br />display-friendly name of application instance starting the request |
| Source |  |  |  |  |  |
|  | Application |  |  |  |  |
|  |  | ApplicationId | string | required | 1df240eb-7d0e-42c0-9bb3-f22ad30fd353<br />unique identifier of application that is logging |
|  |  | ApplicationName | string | required | InvoiceApp<br />display-friendly nameof application that is logging  |
|  | Instance |  |  |  |  |
|  |  | InstanceId | string | required | e75ab07e-e96c-462c-962c-393ecfdc5726<br />  |
|  |  | InstanceName | string | required | InvoiceApp-instance-3<br />display-friendly name of instance of the application that is logging  |
|  | Component |  |  |  |  |
|  |  | ComponentId | string | optional | 9da00df0-6bb3-4b22-be90-3f3bc70329ad<br />unique identifier of component in the application that is logging |
|  |  | ComponentName | string | optional | InvoiceUpdater<br />display-friendly name of the component in the application that is logging |
| Host |  |  |  |  |  |
|  | IPAddress |  | string | optional | 212.25.126.45<br />IP-address of logging application |
|  | ProcessId |  | string | optional | 4956 |
|  | ThreadId |  | string | optional | 458793 |
|  |  |  |  |  |  |
| Body |  |  |  |  |  |
|  | Level |  | numeric | required | severity  of the message <br />Trace = 0,  <br />Debug = 1, <br /> Information = 2, <br /> Warning =  3, <br /> Error =  4, <br /> Critical =  5  |
|  | MessageVersion |  | string | required | Version of the  message |
|  | User |  |  |  |  |
|  |  | UserName | string | optional | rc01245<br />useraccount  |
|  |  | IPAddress | string | optional | 63.25.149.53<br />IP-address of the user |
|  | Message |  |  |  |  |
|  |  | Type | string | optional | invoice:updated |
|  |  | Content | <b style="color:red;">ApplicationLogging: json<br />SystemLogging: string  </b>| non-null | <b style="color:red;">ApplicationLogging:\{ id: 4856, oldvalue: “something”, newvalue: ...\}<br />SystemLogging: "Something went terribly wrong" </b>|
|  |  | Format | string | optional | json<br />format used by content |
|  |




***
* <a name="V1ApplicationLoggingCodeExampleCSharp"> V1 - ApplicationLogging Code example C#

```csharp
using System;
using RestSharp;
using Newtonsoft.Json;

namespace tmp
{
    class Program
    {
        static void Main(string[] args)
        {

            JObject messageJson = JObject.Parse( @"{
                            'Header': {
                                'Correlation': {
                                    'Instance': {
                                        'InstanceName': 'ff362e2a',
                                        'InstanceId': '12321'
                                    },
                                    'CorrelationId': 'ff9a024750fdff9a024750fd',
                                    'Application': {
                                        'ApplicationName': 'persoon',
                                        'ApplicationId': 'ff9a024750fd'
                                    }
                                },
                                'Version': '1',
                                'Index': 'crspersoon-applicationlogging-v1',
                                'Host': {
                                    'ThreadId': '12',
                                    'ProcessId': '1233',
                                    'IPAddress': '127.0.0.1'
                                },
                                'TimeStamp': '2020-02-20T13:57:45.477Z',
                                'Source': {
                                    'Instance': {
                                        'InstanceName': 'crspersoon',
                                        'InstanceId': '12321'
                                    },
                                    'Component': {
                                        'ComponentName': 'Digipolis.crspersoon.Businesslogic.AdresHome',
                                        'ComponentId': '1244'
                                    },
                                    'Application': {
                                        'ApplicationName': 'crspersoon',
                                        'ApplicationId': 'f3da41882748'
                                    }
                                }
                            },
                            'Body': {
                                'MessageVersion': '2',
                                'User': {
                                    'UserName': 'rc01234',
                                    'IPAddress': '127.0.0.1'
                                },
                                'Message': {
                                    'Type': 'This is my message',
                                    'Format': 'json',
                                    'Content': {

                                    }
                                },
                                'Level': 1
                            }
                        }");

            var messageString = JsonConvert.SerializeObject(messageJson);
            var client = new RestClient("https://api-gw-o.antwerpen.be/acpaas/applicationlogging/v1/");
            client.Timeout = -1;
            client.FollowRedirects = true;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("apikey", "[xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx]");

            request.AddParameter("application/json", messageString, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

        }
    }
}
```

* <a name="V1ApplicationLoggingCodeExampleNodeJs"/>V1 - ApplicationLogging Code example NodeJs

```javascript
var request = require('request');
var jsonobj = {
                Header: {
                    Correlation: {
                        Instance: {
                            InstanceName: "ff362e2a",
                            InstanceId: "12321"
                        },
                        CorrelationId: "ff9a024750fdff9a024750fd",
                        Application: {
                            ApplicationName: "persoon",
                            ApplicationId: "ff9a024750fd"
                        }
                    },
                    Version: "1",
                    Index: "crspersoon-applicationlogging-v1",
                    Host: {
                        ThreadId: "12",
                        ProcessId: "1233",
                        IPAddress: "127.0.0.1"
                    },
                    TimeStamp: "2020-02-20T13:57:45.477Z",
                    Source: {
                        Instance: {
                            InstanceName: "crspersoon",
                            InstanceId: "12321"
                        },
                        Component: {
                            ComponentName: "Digipolis.crspersoon.Businesslogic.AdresHome",
                            ComponentId: "1244"
                        },
                        Application: {
                            ApplicationName: "crspersoon",
                            ApplicationId: "f3da41882748"
                        }
                    }
                },
                Body: {
                    MessageVersion: "2",
                    User: {
                        UserName: "rc01234",
                        IPAddress: "127.0.0.1"
                    },
                    Message: {
                        Type: "This is my message",
                        Format: "json",
                        Content: {

                        }
                    },
                    Level: 1
                }
}

var bodystring = JSON.stringify(jsonobj)
var options = {
    'method': 'POST',
    'url': 'https://api-gw-o.antwerpen.be/acpaas/applicationlogging/v1/',
    'headers': {
        'Accept': 'application/json',
        'Content-Type': 'application/x-www-form-urlencoded',
        'apikey': '[xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx]',
    },
    body: bodystring

};
request(options, function (error, response) {
    if (error) throw new Error(error);
    console.log(response.body);
});
```

* <a name="V1SystemLoggingCodeExampleCSharp">V1 - SystemLogging Code example C#
```csharp
using System;
using RestSharp;
using Newtonsoft.Json;

namespace tmp
{
    class Program
    {
        static void Main(string[] args)
        {

            JObject messageJson = JObject.Parse( @"{
                            'Header': {
                                'Correlation': {
                                    'Instance': {
                                        'InstanceName': 'ff362e2a',
                                        'InstanceId': '12321'
                                    },
                                    'CorrelationId': 'ff9a024750fdff9a024750fd',
                                    'Application': {
                                        'ApplicationName': 'persoon',
                                        'ApplicationId': 'ff9a024750fd'
                                    }
                                },
                                'Version': '1',
                                'Index': 'crspersoon-applicationlogging-v1',
                                'Host': {
                                    'ThreadId': '12',
                                    'ProcessId': '1233',
                                    'IPAddress': '127.0.0.1'
                                },
                                'TimeStamp': '2020-02-20T13:57:45.477Z',
                                'Source': {
                                    'Instance': {
                                        'InstanceName': 'crspersoon',
                                        'InstanceId': '12321'
                                    },
                                    'Component': {
                                        'ComponentName': 'Digipolis.crspersoon.Businesslogic.AdresHome',
                                        'ComponentId': '1244'
                                    },
                                    'Application': {
                                        'ApplicationName': 'crspersoon',
                                        'ApplicationId': 'f3da41882748'
                                    }
                                }
                            },
                            'Body': {
                                'MessageVersion': '2',
                                'User': {
                                    'UserName': 'rc01234',
                                    'IPAddress': '127.0.0.1'
                                },
                                'Message': {
                                    'Type': 'messagetype:deleting',
                                    'Format': 'json',
                                    'Content': '{this my message}'
                                },
                                'Level': 1
                            }
                        }");

            var messageString = JsonConvert.SerializeObject(messageJson);
            var client = new RestClient("https://api-gw-o.antwerpen.be/acpaas/systemlogging/v1/");
            client.Timeout = -1;
            client.FollowRedirects = true;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("apikey", "[xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx]");

            request.AddParameter("application/json", messageString, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

        }
    }
}
```
* <a name="V1SystemLoggingCodeExampleNodeJs">V1 - SystemLogging Code example NodeJs
```javascript
var request = require('request');
var jsonobj = {
                Header: {
                    Correlation: {
                        Instance: {
                            InstanceName: "ff362e2a",
                            InstanceId: "12321"
                        },
                        CorrelationId: "ff9a024750fdff9a024750fd",
                        Application: {
                            ApplicationName: "persoon",
                            ApplicationId: "ff9a024750fd"
                        }
                    },
                    Version: "1",
                    Index: "crspersoon-applicationlogging-v1",
                    Host: {
                        ThreadId: "12",
                        ProcessId: "1233",
                        IPAddress: "127.0.0.1"
                    },
                    TimeStamp: "2020-02-20T13:57:45.477Z",
                    Source: {
                        Instance: {
                            InstanceName: "crspersoon",
                            InstanceId: "12321"
                        },
                        Component: {
                            ComponentName: "Digipolis.crspersoon.Businesslogic.AdresHome",
                            ComponentId: "1244"
                        },
                        Application: {
                            ApplicationName: "crspersoon",
                            ApplicationId: "f3da41882748"
                        }
                    }
                },
                Body: {
                    MessageVersion: "2",
                    User: {
                        UserName: "rc01234",
                        IPAddress: "127.0.0.1"
                    },
                    Message: {
                        Type: "messagetype:deleting",
                        Format: "json",
                        Content: "{this is my message}"
                    },
                    Level: 1
                }
}

var bodystring = JSON.stringify(jsonobj)
var options = {
    'method': 'POST',
    'url': 'https://api-gw-o.antwerpen.be/acpaas/systemlogging/v1/',
    'headers': {
        'Accept': 'application/json',
        'Content-Type': 'application/x-www-form-urlencoded',
        'apikey': '[xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx]',
    },
    body: bodystring

};
request(options, function (error, response) {
    if (error) throw new Error(error);
    console.log(response.body);
});
```
 		
 
 