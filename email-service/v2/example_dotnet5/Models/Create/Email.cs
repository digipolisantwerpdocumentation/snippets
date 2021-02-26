using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EmailService.Example.Models.Create
{
    public class Email
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("cc")]
        public string Cc { get; set; }

        [JsonProperty("bcc")]
        public string Bcc { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("ttl")]
        public int Ttl { get; set; } = 24;

        [JsonProperty("priority")]
        public bool Priority { get; set; }

        [JsonProperty("sendAsSingleEmail")]
        public bool SendAsSingleEmail { get; set; }

        [JsonProperty("sendAt")]
        public DateTime? SendAt { get; set; }

        [JsonProperty("recipients")]
        public List<Recipient> Recipients { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }

        [JsonProperty("inlineImages")]
        public List<InlineImage> InlineImages { get; set; }
    }
}
