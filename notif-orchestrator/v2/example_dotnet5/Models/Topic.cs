using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NotificationOrchestrator.Example.Models
{
    public class Topic
    {
        [JsonProperty("supportedLanguages")]
        public List<string> SupportedLanguages { get; set; }

        [JsonProperty("defaultLanguage")]
        public string DefaultLanguage { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("defaultChannel")]
        public string DefaultChannel { get; set; }

        [JsonProperty("supportedChannels")]
        public List<string> SupportedChannels { get; set; }
    }
}
