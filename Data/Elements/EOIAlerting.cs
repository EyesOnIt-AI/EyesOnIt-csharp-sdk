using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIAlerting
    {
        [JsonPropertyName("alert_seconds_count")]
        public double AlertSecondsCount { get; set; }

        [JsonPropertyName("reset_seconds_count")]
        public double ResetSecondsCount { get; set; }

        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("image_notification")]
        public bool ImageNotification { get; set; }

        [JsonPropertyName("last_detection")]
        public EOILastDetection LastDetection { get; set; }

        [JsonPropertyName("alerting")]
        public bool? Alerting { get; set; }

        [JsonPropertyName("genetec")]
        public EOIGenetecAlerting GenetecAlerting { get; set; }

        public EOIAlerting()
        {
            this.PhoneNumber = string.Empty;
            this.GenetecAlerting = new EOIGenetecAlerting();
        }
    }
}
