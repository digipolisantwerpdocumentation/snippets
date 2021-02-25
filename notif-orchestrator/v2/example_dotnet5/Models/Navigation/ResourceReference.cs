using Newtonsoft.Json;

namespace NotificationOrchestrator.Example.Models.Navigation
{
    public class ResourceReference
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
