# WCM Content Manager API example app (Node.js)

## Prerequisites

- Node.js (tested with 14.16.0) or Docker to run the application.
- API key of an application which has a contract with the WCM Content Manager v3 API in ACC.
- Name of your WCM tenant.

## Start example

First add your config to [config.json](config.json).

```
npm install

node index.js
```

Or using Docker:

```
docker build --tag wcm_example_nodejs .

docker run wcm_example_nodejs
```
