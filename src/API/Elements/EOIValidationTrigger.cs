using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIValidationTrigger
    {
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("start_seconds")]
        public int StartSeconds { get; set; }

        [JsonPropertyName("end_seconds")]
        public int EndSeconds { get; set; }
    }
}
