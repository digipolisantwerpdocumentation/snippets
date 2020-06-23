using Newtonsoft.Json;

namespace DigitalVaultExample.models
{
    public class DocumentCheckResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("exist")]
        public bool Exist { get; set; }
    }
}
