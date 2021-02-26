using Newtonsoft.Json;

namespace EmailService.Example.Models.Create
{
    public class InlineImage
    {
        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("cid")]
        public string Cid { get; set; }
    }
}
