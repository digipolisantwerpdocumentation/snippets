using Newtonsoft.Json;

namespace InAppNotification.Example.Models.Get.Navigation
{
    /// <summary>
    /// HAL-page object
    /// </summary>
    public class Page
    {
        [JsonProperty("number")]
        public int Number { get; set; }
        [JsonProperty("size")]
        public int Size { get; set; }
        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
        [JsonProperty("totalElements")]
        public int TotalElements { get; set; }
    }
}
