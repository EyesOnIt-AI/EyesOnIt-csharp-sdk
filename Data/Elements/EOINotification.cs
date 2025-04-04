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
        public EOIGenetecNotification GenetecNotification { get; set; }

        [JsonPropertyName("rest_url")]
        public string RESTUrl { get; set; }
    }
}
