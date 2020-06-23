using Newtonsoft.Json;

namespace DigitalVaultExample.models
{
    public class Metadata
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }

        public Metadata(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public Metadata()
        {
        }
    }
}
