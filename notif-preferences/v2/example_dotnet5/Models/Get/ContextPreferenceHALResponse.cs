using Newtonsoft.Json;
using System.Collections.Generic;

namespace NotificationPreferences.Example.Models.Get
{
    public class ContextPreferenceHALResponse
    {
        [JsonProperty("_links")]
        public Navigation.Links Links { get; set; }

        [JsonProperty("_page")]
        public Navigation.Page Page { get; set; }

        [JsonProperty("_embedded")]
        public ContextPreferencesResponseEmbedded Embedded { get; set; }
    }

    public class ContextPreferencesResponseEmbedded
    {
        [JsonProperty("contextPreferences")]
        public List<ContextPreference> ContextPreferences { get; set; }
    }
}
