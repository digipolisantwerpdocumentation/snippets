using Newtonsoft.Json;

namespace InAppNotification.Example.Models.Get.Navigation
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
        [JsonProperty("prev")]
        public ResourceReference Previous { get; set; }
    }
}
