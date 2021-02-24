using Newtonsoft.Json;

namespace NotificationOrchestrator.Example.Models
{
    public class Push
    {
        [JsonProperty("saveToInbox")]
        public string SaveToInbox { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("applicationId")]
        public string ApplicationId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }
}