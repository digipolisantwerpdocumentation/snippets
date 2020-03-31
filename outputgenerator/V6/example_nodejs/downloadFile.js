const fs = require('fs');
const axios = require('axios');

async function downloadFile(name, targetname) {
    try {
        const writer = fs.createWriteStream(`./target/${targetname}`)
        const response = await axios.get(
            `${process.env.BASEURL}/download`,
            {
                params: {
                    name,
                    target: 'upload'
                },
                headers: {
                    apikey: process.env.APIKEY,
                },
                responseType: 'stream'
            }
        );
        response.data.pipe(writer)

        return new Promise((resolve, reject) => {
            writer.on('finish', resolve);
            writer.on('error', reject);
        });
    } catch (e) {
        console.log('downloadFile', e);
    }
}

module.exports = downloadFile;
