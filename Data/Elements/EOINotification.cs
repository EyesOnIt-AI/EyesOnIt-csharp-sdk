using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOINotification
    {
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("image_notification")]
        public bool ImageNotification { get; set; }

        [JsonPropertyName("genetec")]
        public EOIGenetecNotification GenetecAlerting { get; set; }

        [JsonPropertyName("rest_url")]
        public string RESTUrl { get; set; }

        [JsonPropertyName("last_detection")]
        public EOILastDetection LastDetection { get; set; }

        [JsonPropertyName("alerting")]
        public bool? Alerting { get; set; }
    }
}
