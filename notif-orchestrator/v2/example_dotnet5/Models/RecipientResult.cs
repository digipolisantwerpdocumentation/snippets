using Newtonsoft.Json;

namespace NotificationOrchestrator.Example.Models
{
    public class RecipientResult: Recipient
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
