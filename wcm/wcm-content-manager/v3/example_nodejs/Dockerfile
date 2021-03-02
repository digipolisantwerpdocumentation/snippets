FROM node:14.16.0-alpine

WORKDIR /usr/app

COPY package.json .

RUN npm i --quiet

COPY . .

ENTRYPOINT ["node", "index.js"]
CMD []
