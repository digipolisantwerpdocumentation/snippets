const FormData = require('form-data');
const fs = require('fs');
const axios = require('axios');
const downloadFile = require('./downloadFile');

async function GeneratePDFFromWordTemplate(templateFile, dataFile) {
    try {

        const dataFileFilePath = `./_content/${dataFile}`;
        const templateFilePath = `./_content/${templateFile}`;

        // Create multipart Form
        const formData = new FormData();
        formData.append("resultType", "pdf");
        formData.append("async", 'false');
        formData.append("data", fs.createReadStream(dataFileFilePath), { knownLength: fs.statSync(dataFileFilePath).size });
        formData.append("wordTemplateData", fs.createReadStream(templateFilePath), { knownLength: fs.statSync(templateFilePath).size });

        // Add api key to headers
        const headers = {
            apikey: process.env.APIKEY,
            ...formData.getHeaders(),
        };

        // POST
        const { data } = await axios.post(
            `${process.env.BASEURL}/generator/directWordGeneration`,
            formData,
            { headers }
        );

        // Download result
        await downloadFile(data.value.uploadUri, data.value.name)
    } catch (e) {
        console.log('GeneratePDFFromWordTemplate error', e);
    }
}

GeneratePDFFromWordTemplate('word_simple_template.docx', 'word_simple_template_data.json')
