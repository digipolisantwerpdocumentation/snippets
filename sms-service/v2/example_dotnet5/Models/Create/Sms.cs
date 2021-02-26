using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SmsService.Example.Models.Create
{
    public class Sms
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("ttl")]
        public int Ttl { get; set; } = 24;

        [JsonProperty("priority")]
        public bool Priority { get; set; }

        [JsonProperty("sendAt")]
        public DateTime? SendAt { get; set; }

        [JsonProperty("recipients")]
        public List<Recipient> Recipients { get; set; }
    }
}
