using Newtonsoft.Json;

namespace NotificationPreferences.Example.Models.Navigation
{
    public class ResourceReference
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
