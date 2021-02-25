using Newtonsoft.Json;
using NotificationPreferences.Example.Models.Navigation;
using System;
using System.Collections.Generic;

namespace NotificationPreferences.Example.Models.Get
{
    public class ContextPreference
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("contextName")]
        public string ContextName { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("_links")]
        public ContextPreferenceLinks Links { get; set; }

        [JsonProperty("_embedded")]
        public ContextReferenceEmbedded Embedded { get; set; }
    }

    /// <summary>
    /// HAL-links object
    /// </summary>
    public class ContextPreferenceLinks
    {
        [JsonProperty("topic")]
        public ResourceReference Topic { get; set; }
        [JsonProperty("user")]
        public ResourceReference User { get; set; }
        [JsonProperty("self")]
        public ResourceReference Self { get; set; }
    }

    public class ContextReferenceEmbedded
    {
        [JsonProperty("context")]
        public Context Context { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }

    public class Context
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("defaultChannel")]
        public string DefaultChannel { get; set; }

        [JsonProperty("supportedChannels")]
        public List<string> SupportedChannels { get; set; }

        [JsonProperty("defaultLanguage")]
        public string DefaultLanguage { get; set; }

        [JsonProperty("supportedLanguages")]
        public List<string> SupportedLanguages { get; set; }

        [JsonProperty("whitelist")]
        public List<string> Whitelist { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("_links")]
        public ContextLinks Links { get; set; }
    }

    /// <summary>
    /// HAL context-links object
    /// </summary>
    public class ContextLinks
    {
        [JsonProperty("contextPreferences")]
        public ResourceReference ContextPreferences { get; set; }
        [JsonProperty("self")]
        public ResourceReference Self { get; set; }
    }

    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("cellPhone")]
        public string CellPhone { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("_links")]
        public UserLinks Links { get; set; }
    }

    /// <summary>
    /// HAL user-links object
    /// </summary>
    public class UserLinks
    {
        [JsonProperty("topicPreferences")]
        public ResourceReference TopicPreferences { get; set; }
        [JsonProperty("contextPreferences")]
        public ResourceReference ContextPreferences { get; set; }
        [JsonProperty("self")]
        public ResourceReference Self { get; set; }
    }
}
