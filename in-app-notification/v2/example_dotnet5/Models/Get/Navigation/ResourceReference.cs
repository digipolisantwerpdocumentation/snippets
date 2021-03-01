using Newtonsoft.Json;

namespace InAppNotification.Example.Models.Get.Navigation
{
    public class ResourceReference
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
