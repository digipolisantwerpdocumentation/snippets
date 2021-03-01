using Newtonsoft.Json;
using System.Collections.Generic;

namespace InAppNotification.Example.Models.Get
{
    public class InboxMessagesHALResponse
    {
        [JsonProperty("_links")]
        public Navigation.Links Links { get; set; }

        [JsonProperty("_page")]
        public Navigation.Page Page { get; set; }

        [JsonProperty("_embedded")]
        public InboxMessagesEmbedded Embedded { get; set; }
    }

    public class InboxMessagesEmbedded
    {
        [JsonProperty("messages")]
        public List<InboxMessage> Messages { get; set; }
    }
}
