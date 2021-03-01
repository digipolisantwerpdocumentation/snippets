using Newtonsoft.Json;

namespace PushNotification.Example.Models.Create
{
    public class Recipient
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }
    }
}
