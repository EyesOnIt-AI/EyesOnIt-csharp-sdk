using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIRegion
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("polygon")]
        public EOIVertex[] Polygon { get; set; }

        [JsonPropertyName("motion_detection")]
        public EOIMotionDetection MotionDetection { get; set; }

        [JsonPropertyName("detection_configs")]
        public EOIDetectionConfig[] DetectionConfigs { get; set; }
    }
}
