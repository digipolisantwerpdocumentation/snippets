using Newtonsoft.Json;

namespace NotificationOrchestrator.Example.Models
{
    public class InApp
    {
        [JsonProperty("saveToInbox")]
        public string SaveToInbox { get; set; }

        [JsonProperty("message")]
        public InAppMessage Message { get; set; }

        [JsonProperty("applicationId")]
        public string ApplicationId { get; set; }
    }

    public class InAppMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("link")]
        public InAppMessagelink Link { get; set; }
    }

    public class InAppMessagelink
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("external")]
        public bool External { get; set; }
    }
}