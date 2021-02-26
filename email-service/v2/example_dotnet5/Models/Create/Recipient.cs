using Newtonsoft.Json;

namespace EmailService.Example.Models.Create
{
    public class Recipient
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }
    }
}
