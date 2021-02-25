using Newtonsoft.Json;

namespace NotificationOrchestrator.Example.Models
{
    public class Recipient
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("topicName")]
        public string TopicName { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }
    }
}
