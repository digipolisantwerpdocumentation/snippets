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
        formData.append("async", 'true');
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
        const index = data.lastIndexOf("/");
        const fileName = data.substr(index)

        // wait for status (this blocks only example)
        const interval = setInterval(async () => {
            console.log('check if document has completed');
            const response = await axios.get(
                `${process.env.BASEURL}/generator/task/result/${fileName}`,
                {
                    headers: {
                        apikey: process.env.APIKEY,
                    },
                    validateStatus: false,
                    maxRedirects: 0,
                }
            );

            if(response.data.status === 'Failed') {
                clearInterval(interval);
                console.log('Failed');
            }

            if(response.status === 303) {
                clearInterval(interval);
                await downloadFile(response.data.uploadUri, response.data.name)
                console.log('completed');
            }
        }, 500);
    } catch (e) {
        console.log('GeneratePDFFromWordTemplate error', e);
    }
}


GeneratePDFFromWordTemplate('word_simple_template.docx', 'word_simple_template_data.json')
