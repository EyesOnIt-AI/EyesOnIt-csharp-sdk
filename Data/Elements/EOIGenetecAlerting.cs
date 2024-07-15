using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIGenetecAlerting
    {
        [JsonPropertyName("webhook_event_id")]
        public int? WebhookEventId { get; set; }

        [JsonPropertyName("webhook_camera_uuid")]
        public string WebhookCameraUUID { get; set; }

        public EOIGenetecAlerting()
        {
            this.WebhookEventId = null;
            this.WebhookCameraUUID = null;
        }
    }
}
