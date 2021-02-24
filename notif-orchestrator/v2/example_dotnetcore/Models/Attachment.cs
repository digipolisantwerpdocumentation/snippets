using Newtonsoft.Json;

namespace NotificationOrchestrator.Example.Models
{
    public class Attachment
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
