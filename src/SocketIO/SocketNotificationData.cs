using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class SocketGenetecNotificationData
    {
        [JsonPropertyName("webhook_event_id")]
        public int? WebhookEventId { get; set; }

        [JsonPropertyName("webhook_camera_uuid")]
        public string WebhookCameraUuid { get; set; }
    }

    public class SocketNotificationData
    {
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("genetec")]
        public SocketGenetecNotificationData Genetec { get; set; }

        [JsonPropertyName("rest_url")]
        public string RestUrl { get; set; }

        [JsonPropertyName("include_image")]
        public bool? IncludeImage { get; set; }

        [JsonPropertyName("include_count")]
        public bool? IncludeCount { get; set; }
    }
}
