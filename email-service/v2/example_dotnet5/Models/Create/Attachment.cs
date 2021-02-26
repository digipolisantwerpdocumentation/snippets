using Newtonsoft.Json;

namespace EmailService.Example.Models.Create
{
    public class Attachment
    {
        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
