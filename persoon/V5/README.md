# PERSOON

\<CRS Persoon> Consumer

## Table of contents

<!-- toc -->

- [Links](#links)
- [Get Person](#get-person)
  * [by NationalNumber](#get-person-by-nationalNumber)
  * [by CrsNumber](#get-person-by-nationalNumber)

<!-- tocstop -->

## Links

* [Swagger documentation](https://api-store-a.antwerpen.be/#/org/digipolis/api/persoon/v5/documentation)

## Get Person

**API documentation:** [Swagger](https://api-store-a.antwerpen.be/#/org/digipolis/api/persoon/v5/documentation)

### Get person by NationalNumber (refresh from nationalregistry)

**Node.js:**

```javascript
const request = require('request-promise-native');

const config = {
    host: 'https://api-gw-o.antwerpen.be', // Dev endpoint
    baseUrl: '/digipolis/persoon/v5',
    apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the api-store https://api-store-o.antwerpen.be/
}

async function GetPerson(Key) {
    try {
        const options = {
            body: message,
            headers: {
                'Dgp-Correlation': '[Dgp-Correlation-header]', // DGP header module https://bitbucket.antwerpen.be/projects/NPM/repos/astad-dgp-correlation_npm_nodejs/browse
                apikey: config.apiKey
            },
            json: true,
            resolveWithFullResponse: true,
            url: `${config.host}${config.baseUrl}/persons/${key}?refreshfromnationalregistry=true&personkeytype=NationalNumber`,
        };
        const response = await request.get(options);
        if (response.statusCode !== 200) {
            console.log(`Success`);
        }

    } catch (error) {
        console.log(`Post error ${error.message} from event handler`);
    }

}
GetPerson('12345678912');
```

### Get person by CrsNumber (refresh from nationalregistry)

**Node.js:**

```javascript
const request = require('request-promise-native');

const config = {
    host: 'https://api-gw-o.antwerpen.be', // Dev endpoint
    baseUrl: '/digipolis/persoon/v5',
    apiKey: '[xxxxx-xxxxx-xxxxx-xxxxx-xxxxx]' // You can find this in your application on the api-store https://api-store-o.antwerpen.be/
}

async function GetPerson(Key) {
    try {
        const options = {
            body: message,
            headers: {
                'Dgp-Correlation': '[Dgp-Correlation-header]', // DGP header module https://bitbucket.antwerpen.be/projects/NPM/repos/astad-dgp-correlation_npm_nodejs/browse
                apikey: config.apiKey
            },
            json: true,
            resolveWithFullResponse: true,
            url: `${config.host}${config.baseUrl}/persons/${key}?refreshfromnationalregistry=true&personkeytype=CrsNumber`,
        };
        const response = await request.get(options);
        if (response.statusCode !== 200) {
            console.log(`Success`);
        }

    } catch (error) {
        console.log(`Post error ${error.message} from event handler`);
    }

}
GetPerson('321654');
```