using InAppNotification.Example.Models.Get.Navigation;
using Newtonsoft.Json;
using System;

namespace InAppNotification.Example.Models.Get
{
    public class InboxMessage
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("notificationId")]
        public string NotificationId { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("applicationId")]
        public string ApplicationId { get; set; }

        [JsonProperty("sendAt")]
        public DateTime? SendAt { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("priority")]
        public bool Priority { get; set; } = false;

        [JsonProperty("ttl")]
        public int Ttl { get; set; } = 24;

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("recipient")]
        public Recipient Recipient { get; set; }

        [JsonProperty("_links")]
        public InboxMessageLinks Links { get; set; }
    }

    /// <summary>
    /// HAL-links object
    /// </summary>
    public class InboxMessageLinks
    {
        [JsonProperty("self")]
        public ResourceReference Self { get; set; }
    }

    public class Recipient
    {
        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
