using Newtonsoft.Json;
using System.Collections.Generic;

namespace NotificationOrchestrator.Example.Models
{
    public class Email
    {
        public Email()
        {
            Attachments = new List<Attachment>(); // must be initialized; when set to null, notification stays in status "inprogress"
        }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }
    }
}
