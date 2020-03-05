# Assets API example app (Node.js)

## Prerequisites

- Node.js (tested with 12.16.1) or Docker to run the application.
- API key of an application which has a contract with the Assets API in ACC.

## Start example

```
npm install

node index.js --apiKey "<YOUR-API-KEY>"
```

Or using Docker:

```
docker build --tag assets_example_nodejs .

docker run assets_example_nodejs --apiKey "<YOUR-API-KEY>"
```

You can change the default base URL and API key in [config.json](config.json).

## Example output

```
Starting Assets API example app
Using API key "<some API key>"
CreateUploadTicket response (200): <?xml version="1.0" encoding="UTF-8"?>
<response xmlns:dc="http://purl.org/dc/elements/1.1/">
  <header>
    <item_count>1</item_count>
    <item_count_total>1</item_count_total>
    <item_offset>0</item_offset>
    <request_class>mediamosa_rest_call_upload_ticket_create</request_class>
    <request_matched_method>POST</request_matched_method>
    <request_matched_uri>/upload/ticket/create</request_matched_uri>
    <request_result>success</request_result>
    <request_result_description></request_result_description>
    <request_result_id>601</request_result_id>
    <request_uri>[POST] upload/ticket/create</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.0388</request_process_time>
  </header>
  <items>
    <item>
      <action>https://media-a.antwerpen.be/mediafile/upload?upload_ticket=o1wFcSUbSiFTbGfrFWPKpTOJ</action>
      <uploadprogress_url>https://media-a.antwerpen.be/uploadprogress?id=8663294</uploadprogress_url>
      <asset_id>pWgrclhSOvSTKhXcniWq9YMD</asset_id>
      <mediafile_id>n1txYLQPYYjcWXQEUhynsFlb</mediafile_id>
      <ticket_id>o1wFcSUbSiFTbGfrFWPKpTOJ</ticket_id>
      <progress_id>8663294</progress_id>
      <server_id>131</server_id>
    </item>
  </items>
</response>

UploadFile response (200): <?xml version="1.0" encoding="UTF-8"?>
<response xmlns:dc="http://purl.org/dc/elements/1.1/">
  <header>
    <item_count>0</item_count>
    <item_count_total>0</item_count_total>
    <item_offset>0</item_offset>
    <request_class>mediamosa_rest_call_asset_mediafile_upload</request_class>
    <request_matched_method>POST</request_matched_method>
    <request_matched_uri>/mediafile/upload</request_matched_uri>
    <request_result>success</request_result>
    <request_result_description></request_result_description>
    <request_result_id>601</request_result_id>
    <request_uri>[POST] mediafile/upload</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.559</request_process_time>
  </header>
  <items/>
</response>

GetMediafileView (user_id=my-user-id, response=plain) response (200): <?xml version="1.0" encoding="UTF-8"?>
<response xmlns:dc="http://purl.org/dc/elements/1.1/">
  <header>
    <item_count>1</item_count>
    <item_count_total>1</item_count_total>
    <item_offset>0</item_offset>
    <request_class>mediamosa_rest_call_media_view</request_class>
    <request_matched_method>GET</request_matched_method>
    <request_matched_uri>/asset/$asset_id/view</request_matched_uri>
    <request_result>success</request_result>
    <request_result_description></request_result_description>
    <request_result_id>601</request_result_id>
    <request_uri>[GET] asset/pWgrclhSOvSTKhXcniWq9YMD/view?user_id=my-user-id&amp;response=plain&amp;mediafile_id=n1txYLQPYYjcWXQEUhynsFlb</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.0391</request_process_time>
  </header>
  <items>
    <item>
      <output>https://media-a.antwerpen.be/media/15/n/n1txYLQPYYjcWXQEUhynsFlb/image.png</output>
      <content_type>image/png</content_type>
      <ticket_id>n1txYLQPYYjcWXQEUhynsFlb</ticket_id>
      <static>0</static>
    </item>
  </items>
</response>

GetMediafileView (user_id=my-user-id, response=still) response (200): <?xml version="1.0" encoding="UTF-8"?>
<response xmlns:dc="http://purl.org/dc/elements/1.1/">
  <header>
    <item_count>1</item_count>
    <item_count_total>1</item_count_total>
    <item_offset>0</item_offset>
    <request_class>mediamosa_rest_call_media_view</request_class>
    <request_matched_method>GET</request_matched_method>
    <request_matched_uri>/asset/$asset_id/view</request_matched_uri>
    <request_result>success</request_result>
    <request_result_description></request_result_description>
    <request_result_id>601</request_result_id>
    <request_uri>[GET] asset/pWgrclhSOvSTKhXcniWq9YMD/view?user_id=my-user-id&amp;response=still&amp;mediafile_id=n1txYLQPYYjcWXQEUhynsFlb</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.0404</request_process_time>
  </header>
  <items>
    <item>
      <output>https://media-a.antwerpen.be/media/15/E/E1HqhaXX8ARSfhjdTRx0Gtu8/E1HqhaXX8ARSfhjdTRx0Gtu8.jpg</output>
      <content_type>image/jpeg</content_type>
      <ticket_id>E1HqhaXX8ARSfhjdTRx0Gtu8</ticket_id>
      <stills id="1">
        <still_id>E1HqhaXX8ARSfhjdTRx0Gtu8</still_id>
        <asset_id>pWgrclhSOvSTKhXcniWq9YMD</asset_id>
        <app_id>15</app_id>
        <owner_id>my-user-id</owner_id>
        <filename>E1HqhaXX8ARSfhjdTRx0Gtu8.jpg</filename>
        <mediafile_id_source>n1txYLQPYYjcWXQEUhynsFlb</mediafile_id_source>
        <tag></tag>
        <uri></uri>
        <is_protected>FALSE</is_protected>
        <mediafile_id>n1txYLQPYYjcWXQEUhynsFlb</mediafile_id>
        <width>354</width>
        <height>360</height>
        <filesize>47183</filesize>
        <mime_type>image/jpeg</mime_type>
        <still_time_code></still_time_code>
        <still_order></still_order>
        <still_format>jpg</still_format>
        <still_type>NONE</still_type>
        <still_ticket>https://media-a.antwerpen.be/media/15/E/E1HqhaXX8ARSfhjdTRx0Gtu8/E1HqhaXX8ARSfhjdTRx0Gtu8.jpg</still_ticket>
        <ticket>E1HqhaXX8ARSfhjdTRx0Gtu8</ticket>
      </stills>
    </item>
  </items>
</response>

GetMediafileView (user_id=second-user-id, response=plain) response (200): <?xml version="1.0" encoding="UTF-8"?>
<response xmlns:dc="http://purl.org/dc/elements/1.1/">
  <header>
    <item_count>1</item_count>
    <item_count_total>1</item_count_total>
    <item_offset>0</item_offset>
    <request_class>mediamosa_rest_call_media_view</request_class>
    <request_matched_method>GET</request_matched_method>
    <request_matched_uri>/asset/$asset_id/view</request_matched_uri>
    <request_result>success</request_result>
    <request_result_description></request_result_description>
    <request_result_id>601</request_result_id>
    <request_uri>[GET] asset/pWgrclhSOvSTKhXcniWq9YMD/view?user_id=second-user-id&amp;response=plain&amp;mediafile_id=n1txYLQPYYjcWXQEUhynsFlb</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.0239</request_process_time>
  </header>
  <items>
    <item>
      <output>https://media-a.antwerpen.be/media/15/n/n1txYLQPYYjcWXQEUhynsFlb/image.png</output>
      <content_type>image/png</content_type>
      <ticket_id>n1txYLQPYYjcWXQEUhynsFlb</ticket_id>
      <static>0</static>
    </item>
  </items>
</response>

SetMediafileAcl response (200): <?xml version="1.0" encoding="UTF-8"?>
<response xmlns:dc="http://purl.org/dc/elements/1.1/">
  <header>
    <item_count>1</item_count>
    <item_count_total>1</item_count_total>
    <item_offset>0</item_offset>
    <request_class>mediamosa_rest_call_acl_mediafile_set_rights</request_class>
    <request_matched_method>POST</request_matched_method>
    <request_matched_uri>/mediafile/$mediafile_id/acl</request_matched_uri>
    <request_result>success</request_result>
    <request_result_description></request_result_description>
    <request_result_id>601</request_result_id>
    <request_uri>[POST] mediafile/n1txYLQPYYjcWXQEUhynsFlb/acl</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.0809</request_process_time>
  </header>
  <items>
    <item>
      <acl_user>
        <value>second-user-id</value>
        <result>success</result>
        <result_id>601</result_id>
        <result_description></result_description>
      </acl_user>
    </item>
  </items>
</response>

GetMediafileView (user_id=second-user-id, response=plain) response (200): <?xml version="1.0" encoding="UTF-8"?>
<response xmlns:dc="http://purl.org/dc/elements/1.1/">
  <header>
    <item_count>1</item_count>
    <item_count_total>1</item_count_total>
    <item_offset>0</item_offset>
    <request_class>mediamosa_rest_call_media_view</request_class>
    <request_matched_method>GET</request_matched_method>
    <request_matched_uri>/asset/$asset_id/view</request_matched_uri>
    <request_result>success</request_result>
    <request_result_description></request_result_description>
    <request_result_id>601</request_result_id>
    <request_uri>[GET] asset/pWgrclhSOvSTKhXcniWq9YMD/view?user_id=second-user-id&amp;response=plain&amp;mediafile_id=n1txYLQPYYjcWXQEUhynsFlb</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.0449</request_process_time>
  </header>
  <items>
    <item>
      <output>https://media-a.antwerpen.be/media/ticket/play/15/e/eQG9T5VJGJOXL8USVGvBdLLN/image.png</output>
      <content_type>image/png</content_type>
      <ticket_id>eQG9T5VJGJOXL8USVGvBdLLN</ticket_id>
      <static>0</static>
    </item>
  </items>
</response>

GetMediafileView (user_id=third-user-id, response=plain) response (200): <?xml version="1.0" encoding="UTF-8"?>
<response xmlns:dc="http://purl.org/dc/elements/1.1/">
  <header>
    <item_count>0</item_count>
    <item_count_total>0</item_count_total>
    <item_offset>0</item_offset>
    <request_class>mediamosa_rest_call_media_view</request_class>
    <request_matched_method>GET</request_matched_method>
    <request_matched_uri>/asset/$asset_id/view</request_matched_uri>
    <request_result>error</request_result>
    <request_result_description>Action is not allowed, not authorized (mediafile is protected)</request_result_description>
    <request_result_id>1800</request_result_id>
    <request_uri>[GET] asset/pWgrclhSOvSTKhXcniWq9YMD/view?user_id=third-user-id&amp;response=plain&amp;mediafile_id=n1txYLQPYYjcWXQEUhynsFlb</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.019</request_process_time>
  </header>
  <items/>
</response>

GetMediafileView (user_id=third-user-id, response=plain) failed: Action is not allowed, not authorized (mediafile is protected)
Delete response (200): <?xml version="1.0" encoding="UTF-8"?>
<response xmlns:dc="http://purl.org/dc/elements/1.1/">
  <header>
    <item_count>0</item_count>
    <item_count_total>0</item_count_total>
    <item_offset>0</item_offset>
    <request_class>mediamosa_rest_call_asset_delete</request_class>
    <request_matched_method>POST</request_matched_method>
    <request_matched_uri>/asset/$asset_id/delete</request_matched_uri>
    <request_result>success</request_result>
    <request_result_description></request_result_description>
    <request_result_id>601</request_result_id>
    <request_uri>[POST] asset/pWgrclhSOvSTKhXcniWq9YMD/delete</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.0703</request_process_time>
  </header>
  <items/>
</response>

GetQuota response (200): <?xml version="1.0" encoding="UTF-8"?>
<response xmlns:dc="http://purl.org/dc/elements/1.1/">
  <header>
    <item_count>1</item_count>
    <item_count_total>1</item_count_total>
    <item_offset>0</item_offset>
    <request_class>mediamosa_rest_call_app_get_quota</request_class>
    <request_matched_method>GET</request_matched_method>
    <request_matched_uri>/app/quota</request_matched_uri>
    <request_result>success</request_result>
    <request_result_description></request_result_description>
    <request_result_id>601</request_result_id>
    <request_uri>[GET] app/quota</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.0096</request_process_time>
  </header>
  <items>
    <item>
      <app_quota_mb>0</app_quota_mb>
      <app_diskspace_used_mb>4844.9105615616</app_diskspace_used_mb>
      <quota_available_mb>-4844.9105615616</quota_available_mb>
    </item>
  </items>
</response>
```
