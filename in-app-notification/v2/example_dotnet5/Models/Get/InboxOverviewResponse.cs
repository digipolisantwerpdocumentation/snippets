using Newtonsoft.Json;

namespace InAppNotification.Example.Models.Get
{
    public class InboxOverviewResponse
    {
        [JsonProperty("read")]
        public int Read { get; set; }

        [JsonProperty("unread")]
        public int Unread { get; set; }
    }
}
