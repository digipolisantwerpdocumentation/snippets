
# GDP Dossier

## Table of contents


<!-- toc -->

- [Links](#links)
- [Get Dossiers](#get-dossiers)

<!-- tocstop -->

## Links

* [General info](https://wiki.antwerpen.be/ACPAAS/index.php/Start_als_nieuwe_developer_op_GDP)
* [Swagger documentation](https://api-store-a.antwerpen.be/#/org/gdp/api/tng-dossierapi/v1/documentation)

## Get Dossiers

**API documentation:** [Swagger](https://api-store-a.antwerpen.be/#/org/gdp/api/tng-dossierapi/v1/documentation)

### code example


**Node.js:**

```javascript
const request = require('request-promise-native');

const config = {
    host: 'https://api-gw-a.antwerpen.be', // Acc endpoint
    baseUrl: '/gdp/dossierapi-tng/v1/',
    Authorization: 'Bearer + Token'
}

async function getDdossiers() {
    try {
        const options = {
            headers: {
                Authorization: config.Authorization
            },
            json: true,
            resolveWithFullResponse: true,
            url: `${config.host}${config.baseUrl}/dossiers`,
        };
        const response = await request.get(options);
        if (response.statusCode !== 200) {
            console.log(`Success`);
        }

    } catch (error) {
        console.log(`Error ${error.message}`);
    }

}
getDdossiers();
```
## Get Dossier by id

**API documentation:** [Swagger](https://api-store-a.antwerpen.be/#/org/gdp/api/tng-dossierapi/v1/documentation)

### code example


**Node.js:**

```javascript
const request = require('request-promise-native');

const config = {
    host: 'https://api-gw-a.antwerpen.be', // Acc endpoint
    baseUrl: '/gdp/dossierapi-tng/v1/',
    Authorization: 'Bearer + Token'
}

async function getDdossier(id) {
    try {
        const options = {
            headers: {
                Authorization: config.Authorization
            },
            json: true,
            resolveWithFullResponse: true,
            url: `${config.host}${config.baseUrl}/dossiers/${id}`,
        };
        const response = await request.get(options);
        if (response.statusCode !== 200) {
            console.log(`Success`);
        }

    } catch (error) {
        console.log(`Error ${error.message}`);
    }

}
getDdossier(123);
```
