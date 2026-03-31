using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOINotification
    {
        [JsonPropertyName("last_detection")]
        public EOILastDetectionInfo LastDetection { get; set; }

        [JsonPropertyName("alerting")]
        public bool Alerting { get; set; }

        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        [Obsolete("Genetec-specific notification configuration is not part of the canonical TypeScript SDK surface.")]
        [JsonPropertyName("genetec")]
        public EOIGenetecNotification GenetecNotification { get; set; }

        [JsonPropertyName("rest_url")]
        public string RestUrl { get; set; } 

        [JsonPropertyName("include_image")]
        public bool IncludeImage { get; set; }

        [Obsolete("Count notifications are not part of the canonical TypeScript SDK surface.")]
        [JsonPropertyName("include_count")]
        public bool IncludeCount { get; set; }
    }
}
