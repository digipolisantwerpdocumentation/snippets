# Assets API example app (.NET Core)

## Prerequisites

- .NET Core 3.0 SDK or Docker to build and run the application.
- API key of an application which has a contract with the Assets API in ACC.

## Start example

```
dotnet run -- --api-key "<YOUR-API-KEY>"
```

Or using Docker:

```
docker build --tag assets_example_dotnetcore .

docker run assets_example_dotnetcore --api-key "<YOUR-API-KEY>"
```

You can change the default base URL and API key in [Config.cs](Config.cs).

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
    <request_process_time>0.0375</request_process_time>
  </header>
  <items>
    <item>
      <action>https://media-a.antwerpen.be/mediafile/upload?upload_ticket=W9Dhe4sU9F8CAYYirmFwFvK0</action>
      <uploadprogress_url>https://media-a.antwerpen.be/uploadprogress?id=9980626</uploadprogress_url>
      <asset_id>u1mkXRZCcJNEaXb5JMtqrH3K</asset_id>
      <mediafile_id>O2RWXgWpZXdXHUmVYQ8afrRv</mediafile_id>
      <ticket_id>W9Dhe4sU9F8CAYYirmFwFvK0</ticket_id>
      <progress_id>9980626</progress_id>
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
    <request_process_time>0.5721</request_process_time>
  </header>
  <items/>
</response>

GetMediafileView (user_id=my-user-id, response=download) response (200): <?xml version="1.0" encoding="UTF-8"?>
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
    <request_uri>[GET] asset/u1mkXRZCcJNEaXb5JMtqrH3K/view?user_id=my-user-id&amp;response=download&amp;mediafile_id=O2RWXgWpZXdXHUmVYQ8afrRv</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.0381</request_process_time>
  </header>
  <items>
    <item>
      <output>https://media-a.antwerpen.be/download/15/O/O2RWXgWpZXdXHUmVYQ8afrRv/image.png</output>
      <content_type>image/png</content_type>
      <ticket_id>O2RWXgWpZXdXHUmVYQ8afrRv</ticket_id>
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
    <request_uri>[GET] asset/u1mkXRZCcJNEaXb5JMtqrH3K/view?user_id=my-user-id&amp;response=still&amp;mediafile_id=O2RWXgWpZXdXHUmVYQ8afrRv</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.0378</request_process_time>
  </header>
  <items>
    <item>
      <output>https://media-a.antwerpen.be/media/15/j/jVPCVYtnVadSatwjrdJkwbdN/jVPCVYtnVadSatwjrdJkwbdN.jpg</output>
      <content_type>image/jpeg</content_type>
      <ticket_id>jVPCVYtnVadSatwjrdJkwbdN</ticket_id>
      <stills id="1">
        <still_id>jVPCVYtnVadSatwjrdJkwbdN</still_id>
        <asset_id>u1mkXRZCcJNEaXb5JMtqrH3K</asset_id>
        <app_id>15</app_id>
        <owner_id>my-user-id</owner_id>
        <filename>jVPCVYtnVadSatwjrdJkwbdN.jpg</filename>
        <mediafile_id_source>O2RWXgWpZXdXHUmVYQ8afrRv</mediafile_id_source>
        <tag></tag>
        <uri></uri>
        <is_protected>FALSE</is_protected>
        <mediafile_id>O2RWXgWpZXdXHUmVYQ8afrRv</mediafile_id>
        <width>354</width>
        <height>360</height>
        <filesize>47183</filesize>
        <mime_type>image/jpeg</mime_type>
        <still_time_code></still_time_code>
        <still_order></still_order>
        <still_format>jpg</still_format>
        <still_type>NONE</still_type>
        <still_ticket>https://media-a.antwerpen.be/media/15/j/jVPCVYtnVadSatwjrdJkwbdN/jVPCVYtnVadSatwjrdJkwbdN.jpg</still_ticket>
        <ticket>jVPCVYtnVadSatwjrdJkwbdN</ticket>
      </stills>
    </item>
  </items>
</response>

GetMediafileView (user_id=second-user-id, response=download) response (200): <?xml version="1.0" encoding="UTF-8"?>
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
    <request_uri>[GET] asset/u1mkXRZCcJNEaXb5JMtqrH3K/view?user_id=second-user-id&amp;response=download&amp;mediafile_id=O2RWXgWpZXdXHUmVYQ8afrRv</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.0393</request_process_time>
  </header>
  <items>
    <item>
      <output>https://media-a.antwerpen.be/download/15/O/O2RWXgWpZXdXHUmVYQ8afrRv/image.png</output>
      <content_type>image/png</content_type>
      <ticket_id>O2RWXgWpZXdXHUmVYQ8afrRv</ticket_id>
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
    <request_uri>[POST] mediafile/O2RWXgWpZXdXHUmVYQ8afrRv/acl</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.0773</request_process_time>
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

GetMediafileView (user_id=second-user-id, response=download) response (200): <?xml version="1.0" encoding="UTF-8"?>
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
    <request_uri>[GET] asset/u1mkXRZCcJNEaXb5JMtqrH3K/view?user_id=second-user-id&amp;response=download&amp;mediafile_id=O2RWXgWpZXdXHUmVYQ8afrRv</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.038</request_process_time>
  </header>
  <items>
    <item>
      <output>https://media-a.antwerpen.be/download/ticket/download/15/Y/Y1gSVVLEPU8nRdSjnUmlPEdy/image.png</output>
      <content_type>image/png</content_type>
      <ticket_id>Y1gSVVLEPU8nRdSjnUmlPEdy</ticket_id>
    </item>
  </items>
</response>

GetMediafileView (user_id=third-user-id, response=download) response (200): <?xml version="1.0" encoding="UTF-8"?>
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
    <request_uri>[GET] asset/u1mkXRZCcJNEaXb5JMtqrH3K/view?user_id=third-user-id&amp;response=download&amp;mediafile_id=O2RWXgWpZXdXHUmVYQ8afrRv</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.016</request_process_time>
  </header>
  <items/>
</response>

GetMediafileView (user_id=third-user-id, response=download) failed: Action is not allowed, not authorized (mediafile is protected)
DeleteAsset response (200): <?xml version="1.0" encoding="UTF-8"?>
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
    <request_uri>[POST] asset/u1mkXRZCcJNEaXb5JMtqrH3K/delete</request_uri>
    <version>3.7.0.2203-rc1dev</version>
    <request_process_time>0.057</request_process_time>
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
    <request_process_time>0.0093</request_process_time>
  </header>
  <items>
    <item>
      <app_quota_mb>0</app_quota_mb>
      <app_diskspace_used_mb>4844.202246666</app_diskspace_used_mb>
      <quota_available_mb>-4844.202246666</quota_available_mb>
    </item>
  </items>
</response>
```
