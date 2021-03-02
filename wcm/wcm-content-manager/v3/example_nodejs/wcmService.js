const axios = require("axios");

class WcmService {
  constructor(config) {
    this.axiosInstance = axios.create({
      baseURL: config.baseAddress,
      headers: {
        ApiKey: config.apiKey,
        tenant: config.tenant,
      },
    });
  }

  async createContentItem(content) {
    const response = await this.axiosInstance.post('content', content);
    console.log(`createContentItem response (${response.status}): ${JSON.stringify(response.data)}`);
    // createContentItem response (201): {"_id":"6037bced15a804d1f0765305","fields":{"test":"Some test"},"meta":{"contentType":{"_id":"5b921066534a07e22c43aece","versions":[],"fields":[{"_id":"test","validation":{"required":false},"type":"text","label":"test","operators":[{"label":"equals","value":"equals"},{"label":"contains","value":"i"},{"label":"starts with","value":"^"},{"label":"ends with","value":"$"}],"dataType":"string","indexed":false,"multiLanguage":false,"options":[],"max":1,"min":1,"taxonomyLists":[],"uuid":"27357f73-91ea-4e0c-89f2-f1a0d079f7d6"}],"meta":{"label":"testtype","description":"test type","safeLabel":"testtype","lastEditor":"5a002a269062ab6b7212d2dd","canBeFiltered":true,"deleted":false,"hitCount":0,"taxonomy":{"tags":[],"fieldType":"Taxonomy","available":[]},"lastModified":"2018-09-07T05:45:10.602Z","created":"2018-09-07T05:45:10.602Z"},"uuid":"39ea4178-3fa1-4d8d-b2f0-3a4285c3de2c","__v":0},"publishDate":"2020-10-20T11:00:00.000Z","label":"TestCodeSnippet","status":"PUBLISHED","safeLabel":"test_code_snippet","lastEditor":"111111111111111111111111","firstPublished":"2021-02-25T15:06:21.079Z","parents":{"views":[],"content":[]},"deleted":false,"hasDetail":false,"activeLanguages":["NL"],"hitCount":0,"hasScheduled":false,"published":true,"lastModified":"2021-02-25T15:06:21.077Z","created":"2021-02-25T15:06:21.077Z","taxonomy":{"tags":[],"dataType":"taxonomy","available":[]},"slug":{"NL":"test-code-snippet","multiLanguage":true}},"uuid":"5d394e32-6e30-48b2-8994-7e25f227c115","__v":0}

    return response.data;
  }

  async getContentItem(uuid) {
    const response = await this.axiosInstance.get(`content/${uuid}`, {
      params: {
        lang: 'NL',
        populate: true,
      },
    });
    console.log(`getContentItem response (${response.status}): ${JSON.stringify(response.data)}`);
    // getContentItem response (200): {"_id":"6037bced15a804d1f0765305","fields":{"test":"Some test"},"meta":{"publishDate":"2020-10-20T11:00:00.000Z","label":"TestCodeSnippet","status":"PUBLISHED","safeLabel":"test_code_snippet","firstPublished":"2021-02-25T15:06:21.079Z","parents":{"views":[],"content":[]},"deleted":false,"hasDetail":false,"activeLanguages":["NL"],"hitCount":0,"hasScheduled":false,"published":true,"lastModified":"2021-02-25T15:06:21.077Z","created":"2021-02-25T15:06:21.077Z","taxonomy":{"tags":[]},"slug":"test-code-snippet","historyRef":"efaea42b-9cfd-435e-8390-37b495221921","contentType":"testtype","contentTypeUuid":"39ea4178-3fa1-4d8d-b2f0-3a4285c3de2c"},"uuid":"5d394e32-6e30-48b2-8994-7e25f227c115"}

    return response.data;
  }

  async deleteContentItem(uuid) {
    const response = await this.axiosInstance.delete(`content/${uuid}`);
    console.log(`deleteContentItem response (${response.status})`);
    // deleteContentItem response (204)

    return response.data;
  }
}

module.exports = WcmService;
