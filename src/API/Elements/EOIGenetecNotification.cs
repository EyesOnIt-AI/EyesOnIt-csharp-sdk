using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIGenetecNotification
    {
        [JsonPropertyName("webhook_event_id")]
        public int? WebhookEventId { get; set; }

        [JsonPropertyName("webhook_camera_uuid")]
        public string WebhookCameraUUID { get; set; }

        public EOIGenetecNotification()
        {
            this.WebhookEventId = null;
            this.WebhookCameraUUID = null;
        }
    }
}
