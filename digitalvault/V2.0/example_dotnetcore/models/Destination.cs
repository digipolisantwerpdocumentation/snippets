using Newtonsoft.Json;
using System.Collections.Generic;

namespace DigitalVaultExample.models
{
    public class Destination
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("autoCommit")]
        public bool AutoCommit { get; set; }
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
        [JsonProperty("notificationNeeded")]
        public bool NotificationNeeded { get; set; }

        public Destination()
        {
            AutoCommit = false;
            Tags = new List<string>();
            NotificationNeeded = false;
        }
    }
}
