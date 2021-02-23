#  WCM Proxy

Gebruik deze API als je een frontend (of andere consumer app) wil maken bovenop de WCM. Deze API bevat vooral functionaliteiten om informatie op te halen. Het beheren van deze informati doe je dan via de WCM Editor (https://wcmv3-a.antwerpen.be). Dit is een User Interface waarmee technische en niet technische mensen mee kunnen werken.


## Links

* [General info](https://acpaas.digipolis.be/nl/product/web-content-management-engine/v3.5.1/api-web-content-manager-proxy-v-3/about)
* [Swagger documentation](https://api-store-o.antwerpen.be/#/org/district01/api/wcmproxy/v3/documentation)

## Get content

**API documentation:** [Swagger](https://api-store-o.antwerpen.be/#/org/district01/api/wcmproxy/v3/documentation)


**Node.js:**

```javascript
const request = require('request-promise-native');

const config = {
    host: 'https://api-gw-o.antwerpen.be', // Dev endpoint
    baseUrl: '/district01/wcm-proxy/v3',
 	api-store: 'https://api-store-o.antwerpen.be/'
}

function getaccessToken(){
	// get your access token here by client-credentials
 	return 'token'
}

async function getContent($page) {
    try {
        const options = {
            headers: {
                copy.headers.Authorization = `Bearer ${getaccessToken()}`
            },
            json: true,
            resolveWithFullResponse: true,
            url: `${config.host}${config.baseUrl}/acpaas/workflow/v3/content?slug=${page}&populate=2&lang=nl`,
        };
        const response = await request.get(options);

    } catch (error) {
        console.log(`Post error ${error.message} from event handler`);
    }

}
getContent('home');
```
## BFF proxy

Zet een proxy router op in je BFF (backend for frontend). Op deze manier kan de frontend aan alle endpoints van WCM zonder dat deze authenticatie details nodig heeft. Dit voorbeeld gebruikt een user-token dat tijdens de login opgehaald werd.

```javascript
import proxy from 'express-http-proxy';
import url from 'url';

const apiUrl = url.parse(process.env.WCM_BASESURL);
const router = proxy(process.env.WCM_BASESURL, {
  proxyReqPathResolver: (req) => {
    const reqUrl = url.parse(req.url);
    return apiUrl.path + reqUrl.path;
  },
  proxyReqOptDecorator: async (proxyReq, req) => {
    try {
      const copy = proxyReq;
      copy.headers.apikey = process.env.WCM_APIKEY;
      copy.headers.tenant = process.env.WCM_TENNANT;
      if (req.session.userToken) {
        copy.headers.Authorization = `Bearer ${req.session.userToken.accessToken}`;
        copy.headers.service_type = 'mprofiel';
      }
      return copy;
    } catch (error) {
      console.log(error);
      return proxyReq;
    }
  },
});

export default router;
```
