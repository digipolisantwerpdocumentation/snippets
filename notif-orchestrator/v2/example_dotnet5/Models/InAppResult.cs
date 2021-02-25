using Newtonsoft.Json;

namespace NotificationOrchestrator.Example.Models
{
    public class InAppResult
    {
        [JsonProperty("message")]
        public object Message { get; set; }

        [JsonProperty("applicationId")]
        public string ApplicationId { get; set; }
    }
}