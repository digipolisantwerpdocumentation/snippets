#  Form enigne admin

De Form Engine stelt power users in staat om data entry formulieren incl. complexe validaties te definiëren via een gebruiksvriendelijke WYSIWYG Form Composer editor. Deze forms kunnen met minimale bijstand van software ontwikkelaars ingebed worden in host applicaties mbv de Form Renderer component. Formulieren kunnen gebruik maken van dynamische data en er zijn verschillende instelbare mogelijkheden naar afhandeling van door eindgebruikers ingevulde formulieren toe.


## Links

* [General info](https://wiki.antwerpen.be/ACPAAS/index.php/Form_Engine)
* [Swagger documentation](https://api-store-o.antwerpen.be/#/org/acpaas/api/formandsurveyadminengine/v1/documentation)

## Add application to tennant

**API documentation:** [Swagger](hhttps://api-store-o.antwerpen.be/#/org/acpaas/api/formandsurveyadminengine/v1/documentation)


**Node.js:**

```javascript
const request = require('request-promise-native');

const config = {
    host: 'https://api-gw-o.antwerpen.be', // Dev endpoint
    baseUrl: '/acpaas/form-survey-admin-en/v1',
    apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the 	api-store https://api-store-o.antwerpen.be/
}

function get_auth(){
	// get auth token
	return "token"
}

async function add_application(tenantId, applicationId) {
    try {
        const options = {
            body: {
				applicationId: applicationId,
				tenantId: tenantId,
				canManageTenantConfiguration: true,
				canPublishForms: true,
				canEditForms: true,
				canAccessForms: true
			  },
            headers: {
                apikey: config.apiKey
                Authorization: `Bearer ${get_auth()}`
            },
            json: true,
            resolveWithFullResponse: true,
            url: `${config.host}${config.baseUrl}/admin/application-tenants`,
        };
        const response = await request.post(options);
    } catch (error) {
        console.log(`Post error ${error.message}`);
    }

}
add_application("tenantId", "applicationId");
```
