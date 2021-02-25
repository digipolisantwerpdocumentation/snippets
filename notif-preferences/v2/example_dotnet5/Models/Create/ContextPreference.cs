using Newtonsoft.Json;

namespace NotificationPreferences.Example.Models.Create
{
    public class ContextPreference
    {
        [JsonProperty("contextName")]
        public string ContextName { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}
