using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements.VMS
{
    public class EOIGenetecDetectionConfig
    {
        // Genetec Properties
        [JsonPropertyName("webhook_event_id")]
        public int WebhookEventId { get; set; }


        public EOIGenetecDetectionConfig()
        {
        }
    }
}
