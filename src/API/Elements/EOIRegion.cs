using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIRegion
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("polygon")]
        public EOIVertex[] Polygon { get; set; }

        [JsonPropertyName("motion_detection")]
        public EOIMotionDetection MotionDetection { get; set; }

        [JsonPropertyName("detection_configs")]
        public EOIDetectionConfig[] DetectionConfigs { get; set; }

        public EOIRegion()
        {
            Enabled = true;
        }
    }
}
