using Newtonsoft.Json;

namespace NotificationPreferences.Example.Models.Navigation
{
    /// <summary>
    /// HAL-links object
    /// </summary>
    public class Links
    {
        [JsonProperty("first")]
        public ResourceReference First { get; set; }
        [JsonProperty("last")]
        public ResourceReference Last { get; set; }
        [JsonProperty("self")]
        public ResourceReference Self { get; set; }
        [JsonProperty("next")]
        public ResourceReference Next { get; set; }
        [JsonProperty("prev")]      // in shared version the full word "previous" is used
        public ResourceReference Previous { get; set; }
    }
}
