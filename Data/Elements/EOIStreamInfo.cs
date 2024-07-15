using EyesOnItSDK.Data.Elements;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIStreamInfo
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("regions")]
        public EOIRegion[] Regions { get; set; }

        [JsonPropertyName("object_size")]
        public int ObjectSize { get; set; }

        [JsonPropertyName("prompts")]
        public EOIObjectDescription[] ObjectDescriptions { get; set; }

        [JsonPropertyName("alerting")]
        public EOIAlerting Alerting { get; set; }

        [JsonPropertyName("efficient_detection")]
        public EOIMotionDetection MotionDetection { get; set; }

        [JsonPropertyName("bounding_box")]
        public EOIBoundingBox BoundingBox { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("frame_rate")]
        public int FrameRate { get; set; }

        [JsonPropertyName("gpu_util")]
        public List<int> GpuUtil { get; set; }

        public bool IsMonitoring
        {
            get
            {
                return Status == "MONITORING";
            }
        }

        public bool IsAlerting
        {
            get
            {
                return Alerting != null && Alerting.Alerting != null && Alerting.Alerting == true;
            }
        }

        public static List<EOIStreamInfo> FromJson(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new JsonStringEnumConverter(),
                    new DateTimeConverterUsingDateTimeParse()
                }
            };

            List<EOIStreamInfo> data = jsonString == null ? null : JsonSerializer.Deserialize<List<EOIStreamInfo>>(jsonString, options);

            return data;
        }

        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var jsonData = JsonSerializer.Serialize<EOIStreamInfo>(this, options);

            return jsonData;
        }
    }
}
