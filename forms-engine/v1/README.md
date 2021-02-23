# Forms Engine:


### Links:

<!--ts-->
   * [Get form definition](#send-sms)
   * [General info](https://wiki.antwerpen.be/ACPAAS/index.php/Form_%26_Survey_Engine)
<!--te-->


## Get form definition

**API documentation:** [Swagger](https://api-store-a.antwerpen.be/#/org/acpaas/api/formandsurveyengine/v1/documentation)

```javascript
const request = require('request-promise-native');
const uuid = require('uuid');

const config = {
        baseUrl: 'https://api-gw-a.antwerpen.be/acpaas/form-survey-engine/v1', // Acc endpoint
        form: 'form-x',
        tenant: 'tenant-id',
        apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the api-store https://api-store-o.antwerpen.be/
}

async function getForm() {
    try {
        const dgpCorrelation = correlation.createDgpCorrelation(uuid.v4(), 'Your-service');
        const options = {
            headers: {
       		apikey: config.apiKey,
        		'dgp-tenant-id': config.tenant,
        		Authorization: `Bearer ${token}` // oauth2 token
            },
            json: true,
            resolveWithFullResponse: true,
            url: `${config.baseUrl}/forms/${config.form}/latest`,
        };
        const response = await request.get(options);
    } catch (error) {
        console.log(`error ${error.message}`);
    }
}
getForm();

```
