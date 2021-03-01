using Newtonsoft.Json;
using System.Collections.Generic;

namespace PushNotification.Example.Models.Create
{
    public class PushNotification
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("applicationId")]
        public string ApplicationId { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("ttl")]
        public int Ttl { get; set; } = 24;

        [JsonProperty("recipients")]
        public List<Recipient> Recipients { get; set; }
    }
}
