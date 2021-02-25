using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NotificationOrchestrator.Example.Models
{
    public class SendNotificationResult
    {
        [JsonProperty("_links")]
        public Navigation.Links Links { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("sendAt")]
        public DateTime SendAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("inApp")]
        public InAppResult InApp { get; set; }

        [JsonProperty("sms")]
        public Sms Sms { get; set; }

        [JsonProperty("email")]
        public Email Email { get; set; }

        [JsonProperty("recipients")]
        public List<RecipientResult> Recipients { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("priority")]
        public bool Priority { get; set; } = false;

        [JsonProperty("ttl")]
        public int TimeToLive { get; set; } = 24;
    }
}
