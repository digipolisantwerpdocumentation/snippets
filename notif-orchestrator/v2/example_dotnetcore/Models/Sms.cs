using Newtonsoft.Json;

namespace NotificationOrchestrator.Example.Models
{
    public class Sms
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
