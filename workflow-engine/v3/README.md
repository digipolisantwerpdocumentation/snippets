#  workflow enigne

De Workflow engine is een REST API (gebaseerd op Flowable) waarmee in BPMN getekende processen 'executable' kunnen worden gemaakt. 

De engine biedt een platform voor workflow en Business Process Management (BPM) voor ontwikkelaars, systeemadmins en business users. De component implementeert typische constructies zoals gateways, gebruikerstaken, scripttaken, servicetaken, timers, errors,… Ook monitoring van de processen is mogelijk via de API. 

Een gebruiker tekent zijn proces in een grafische modelleringsomgeving, de XML wordt vervolgens ingebracht als een procesdefinitie in de engine en vanaf dat moment is het proces beschikbaar en kunnen instanties geïnitieerd worden.

## Basisconcept
1. je ontwerpt in de workflow modeler (van flowable) een BPMN diagram
2. via de workflow api instantieer je een instance van die workflow (POST /runtime/process-instances), waaraan je een data payload kan meegeven
3. flowable voert de stappen uit de BPMN uit, gebaseerd op de aangeleverde data, sommige stappen zijn user tasks en blokkeren tot via een API call de task als completed gemarkeerd wordt (PUT /runtime/tasks/id)

## Links

* [General info](https://acpaas.digipolis.be/nl/product/workflow-engine/about)
* [Swagger documentation](https://acpaas.digipolis.be/nl/product/workflow-engine/v3.0.0/api-workflow-engine-v-3/about)

## Start Workflow

**API documentation:** [Swagger](https://acpaas.digipolis.be/nl/product/event-handler-engine/v2.0.0/api-event-handler-v-2/about#/Publish)


**Node.js:**

```javascript
const request = require('request-promise-native');

const config = {
    host: 'https://api-gw-o.antwerpen.be', // Dev endpoint
    baseUrl: '/acpaas/workflow/v3',
    apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the 	api-store https://api-store-o.antwerpen.be/
}

async function start_workflow() {
    try {
        const options = {
            body: {
				  "processDefinitionId": "string",
				  "variables": [
				    {
				      "scope": "string",
				      "name": "string",
				      "type": "string",
				      "valueUrl": "string",
				      "value": {}
				    }
				  ],
				  "returnVariables": false,
				  "businessKey": "string",
				  "message": "string",
				  "processDefinitionKey": "string"
			  },
            headers: {
                apikey: config.apiKey
            },
            json: true,
            resolveWithFullResponse: true,
            url: `${config.host}${config.baseUrl}/acpaas/workflow/v3`,
        };
        const response = await request.post(options);

    } catch (error) {
        console.log(`Post error ${error.message} from event handler`);
    }

}
start_workflow();
```
