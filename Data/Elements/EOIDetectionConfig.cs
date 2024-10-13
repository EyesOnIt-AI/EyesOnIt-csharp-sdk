using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIDetectionConfig
    {
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("class_threshold")]
        public int? ClassThreshold { get; set; }

        [JsonPropertyName("object_size")]
        public int? ObjectSize { get; set; }

        [JsonPropertyName("object_descriptions")]
        public EOIObjectDescription[] ObjectDescriptions { get; set; }

        [JsonPropertyName("conditions")]
        public EOIDetectionCondition[] DetectionConditions{ get; set; }

        [JsonPropertyName("alert_seconds")]
        public float AlertSeconds { get; set; }

        [JsonPropertyName("reset_seconds")]
        public float ResetSeconds{ get; set; }


        public EOIDetectionConfig()
        { 
        }
    }
}
