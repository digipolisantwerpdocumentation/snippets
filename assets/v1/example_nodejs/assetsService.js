const requestPromise = require("request-promise-native");
const parseXmlString = require("xml2js").parseStringPromise;

class AssetsService {
    constructor(config) {
        this.requestClient = requestPromise.defaults({
            baseUrl: config.baseAddress,
            headers: { "apikey": config.apiKey },
            resolveWithFullResponse: true
        });
    }

    async createUploadTicket(userId) {
        const formData = {
            user_id: userId
            // The user_id property can be used to specify the user who uploaded the file
            // The same user ID is also needed for other calls concerning the same file
        };

        const response = await this.requestClient.post("upload/ticket/create", { formData });
        // CreateUploadTicket response (200): <?xml version="1.0" encoding="UTF-8"?><response xmlns:dc="http://purl.org/dc/elements/1.1/"><header><item_count>1</item_count><item_count_total>1</item_count_total><item_offset>0</item_offset><request_class>mediamosa_rest_call_upload_ticket_create</request_class><request_matched_method>POST</request_matched_method><request_matched_uri>/upload/ticket/create</request_matched_uri><request_result>success</request_result><request_result_description></request_result_description><request_result_id>601</request_result_id><request_uri>[POST] upload/ticket/create</request_uri><version>3.7.0.2203-rc1dev</version><request_process_time>0.0388</request_process_time></header><items><item><action>https://media-a.antwerpen.be/mediafile/upload?upload_ticket=o1wFcSUbSiFTbGfrFWPKpTOJ</action><uploadprogress_url>https://media-a.antwerpen.be/uploadprogress?id=8663294</uploadprogress_url><asset_id>pWgrclhSOvSTKhXcniWq9YMD</asset_id><mediafile_id>n1txYLQPYYjcWXQEUhynsFlb</mediafile_id><ticket_id>o1wFcSUbSiFTbGfrFWPKpTOJ</ticket_id><progress_id>8663294</progress_id><server_id>131</server_id></item></items></response>

        return await this.parseResult(response, "CreateUploadTicket");
    }

    async uploadFile(ticketId, file, fileName, createThumbnail = false, thumbnailSize = null) {
        const formData = {
            upload_ticket: ticketId,
            create_still: createThumbnail.toString(),
            file: {
                value: file,
                options: {
                    filename: fileName,
                    contentType: "application/octet-stream"
                }
            }
        };

        if (thumbnailSize != null) {
            formData.size = thumbnailSize;
        }

        const response = await this.requestClient.post("mediafile/upload", { formData });
        // UploadFile response (200): <?xml version="1.0" encoding="UTF-8"?><response xmlns:dc="http://purl.org/dc/elements/1.1/"><header><item_count>0</item_count><item_count_total>0</item_count_total><item_offset>0</item_offset><request_class>mediamosa_rest_call_asset_mediafile_upload</request_class><request_matched_method>POST</request_matched_method><request_matched_uri>/mediafile/upload</request_matched_uri><request_result>success</request_result><request_result_description></request_result_description><request_result_id>601</request_result_id><request_uri>[POST] mediafile/upload</request_uri><version>3.7.0.2203-rc1dev</version><request_process_time>0.559</request_process_time></header><items/></response>

        return await this.parseResult(response, "UploadFile");
    }

    async getMediafileView(assetId, userId, responseType, mediafileId) {
        const response = await this.requestClient.get(`asset/${assetId}/view?user_id=${userId}&response=${responseType}&mediafile_id=${mediafileId}`);
        // GetMediafileView (user_id=my-user-id, response=plain) response (200): <?xml version="1.0" encoding="UTF-8"?><response xmlns:dc="http://purl.org/dc/elements/1.1/"><header><item_count>1</item_count><item_count_total>1</item_count_total><item_offset>0</item_offset><request_class>mediamosa_rest_call_media_view</request_class><request_matched_method>GET</request_matched_method><request_matched_uri>/asset/$asset_id/view</request_matched_uri><request_result>success</request_result><request_result_description></request_result_description><request_result_id>601</request_result_id><request_uri>[GET] asset/pWgrclhSOvSTKhXcniWq9YMD/view?user_id=my-user-id&amp;response=plain&amp;mediafile_id=n1txYLQPYYjcWXQEUhynsFlb</request_uri><version>3.7.0.2203-rc1dev</version><request_process_time>0.0391</request_process_time></header><items><item><output>https://media-a.antwerpen.be/media/15/n/n1txYLQPYYjcWXQEUhynsFlb/image.png</output><content_type>image/png</content_type><ticket_id>n1txYLQPYYjcWXQEUhynsFlb</ticket_id><static>0</static></item></items></response>

        return await this.parseResult(response, `GetMediafileView (user_id=${userId}, response=${responseType})`);
    }

    async setMediafileAcl(mediafileId, userId, aclUserId = null, aclGroupId = null, aclDomain = null, aclRealm = null) {
        const formData = {
            user_id: userId
        };

        if (aclUserId != null) {
            formData.acl_user_id = aclUserId;
        }

        if (aclGroupId != null) {
            formData.acl_group_id = aclGroupId;
        }

        if (aclDomain != null) {
            formData.acl_domain = aclDomain;
        }

        if (aclRealm != null) {
            formData.acl_realm = aclRealm;
        }

        const response = await this.requestClient.post(`mediafile/${mediafileId}/acl`, { formData });
        // SetMediafileAcl response (200): <?xml version="1.0" encoding="UTF-8"?><response xmlns:dc="http://purl.org/dc/elements/1.1/"><header><item_count>1</item_count><item_count_total>1</item_count_total><item_offset>0</item_offset><request_class>mediamosa_rest_call_acl_mediafile_set_rights</request_class><request_matched_method>POST</request_matched_method><request_matched_uri>/mediafile/$mediafile_id/acl</request_matched_uri><request_result>success</request_result><request_result_description></request_result_description><request_result_id>601</request_result_id><request_uri>[POST] mediafile/n1txYLQPYYjcWXQEUhynsFlb/acl</request_uri><version>3.7.0.2203-rc1dev</version><request_process_time>0.0809</request_process_time></header><items><item><acl_user><value>second-user-id</value><result>success</result><result_id>601</result_id><result_description></result_description></acl_user></item></items></response>

        return await this.parseResult(response, "SetMediafileAcl");
    }

    async delete(assetId, userId) {
        const formData = {
            user_id: userId,
            delete: "cascade"
        };

        const response = await this.requestClient.post(`asset/${assetId}/delete`, { formData });
        // Delete response (200): <?xml version="1.0" encoding="UTF-8"?><response xmlns:dc="http://purl.org/dc/elements/1.1/"><header><item_count>0</item_count><item_count_total>0</item_count_total><item_offset>0</item_offset><request_class>mediamosa_rest_call_asset_delete</request_class><request_matched_method>POST</request_matched_method><request_matched_uri>/asset/$asset_id/delete</request_matched_uri><request_result>success</request_result><request_result_description></request_result_description><request_result_id>601</request_result_id><request_uri>[POST] asset/pWgrclhSOvSTKhXcniWq9YMD/delete</request_uri><version>3.7.0.2203-rc1dev</version><request_process_time>0.0703</request_process_time></header><items/></response>

        return await this.parseResult(response, "Delete");
    }

    async getQuota() {
        const response = await this.requestClient.get("app/quota");
        // GetQuota response (200): <?xml version="1.0" encoding="UTF-8"?><response xmlns:dc="http://purl.org/dc/elements/1.1/"><header><item_count>1</item_count><item_count_total>1</item_count_total><item_offset>0</item_offset><request_class>mediamosa_rest_call_app_get_quota</request_class><request_matched_method>GET</request_matched_method><request_matched_uri>/app/quota</request_matched_uri><request_result>success</request_result><request_result_description></request_result_description><request_result_id>601</request_result_id><request_uri>[GET] app/quota</request_uri><version>3.7.0.2203-rc1dev</version><request_process_time>0.0096</request_process_time></header><items><item><app_quota_mb>0</app_quota_mb><app_diskspace_used_mb>4844.9105615616</app_diskspace_used_mb><quota_available_mb>-4844.9105615616</quota_available_mb></item></items></response>

        return await this.parseResult(response, "GetQuota");
    }

    async parseResult(response, method) {
        console.log(`${method} response (${response.statusCode}): ${response.body}`);

        const parsedResult = await parseXmlString(response.body, { explicitArray: false });

        if (parsedResult.response.header.request_result === "error") {
            // You should probably throw an exception in real implementations
            console.log(`${method} failed: ${parsedResult.response.header.request_result_description}`);
        }

        return parsedResult;
    }
}

module.exports = AssetsService;
