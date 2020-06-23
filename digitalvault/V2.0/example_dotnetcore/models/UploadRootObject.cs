using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DigitalVaultExample.models
{
    public class UploadRootObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("externalName")]
        public string ExternalName { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("documentType")]
        public string DocumentType { get; set; }
        [JsonProperty("referenceDate")]
        public DateTime ReferenceDate { get; set; }
        [JsonProperty("bulkOperation")]
        public string BulkOperation { get; set; }

        [JsonProperty("destinations")]
        public List<Destination> Destinations { get; set; }
        [JsonProperty("metadata")]
        public List<Metadata> Metadata { get; set; }

    }
}
