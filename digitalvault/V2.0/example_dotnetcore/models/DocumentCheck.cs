using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DigitalVaultExample.models
{
    public class DocumentCheck
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("referenceDate")]
        public DateTime ReferenceDate { get; set; }

        [JsonProperty("destinations")]
        public List<string> Destinations { get; set; }

        [JsonProperty("bulkOperation")]
        public string BulkOperation { get; set; }

        public DocumentCheck()
        {
            Destinations = new List<string>();
        }
    }
}
