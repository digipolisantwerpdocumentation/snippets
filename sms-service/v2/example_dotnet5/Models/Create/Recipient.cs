using Newtonsoft.Json;

namespace SmsService.Example.Models.Create
{
    public class Recipient
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }
    }
}
