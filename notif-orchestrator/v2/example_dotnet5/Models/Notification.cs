using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NotificationOrchestrator.Example.Models
{
    public class Notification
    {
        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("inApp")]
        public InApp InApp { get; set; }

        [JsonProperty("push")]
        public Push Push { get; set; }

        [JsonProperty("sms")]
        public Sms Sms { get; set; }

        [JsonProperty("email")]
        public Email Email { get; set; }

        [JsonProperty("recipients")]
        public List<Recipient> Recipients { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("priority")]
        public bool Priority { get; set; } = false;

        [JsonProperty("ttl")]
        public int TimeToLive { get; set; } = 24;

        [JsonProperty("sendAt")]
        public DateTime? SendAt { get; set; }
    }
}
