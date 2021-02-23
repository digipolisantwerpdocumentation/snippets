# BRaaS API example app (Node.js)

## Prerequisites

- Node.js (tested with 14.16.0) or Docker to run the application.
- API key of an application which has a contract with the BRaaS v3 API in ACC.
- Optionally a JWT token of a user or application which is known in BRaaS. If no JWT token is provided, the application of the API key should be known in BRaaS (as described on the [ACPaaS Portal](https://acpaas.digipolis.be/nl/product/braas-engine/v1.0.0/features#applicatiesubjecten)).

## Start example

First add your API key and (optionally) JWT to [config.json](config.json).

```
npm install

node index.js
```

Or using Docker:

```
docker build --tag braas_example_nodejs .

docker run braas_example_nodejs
```
