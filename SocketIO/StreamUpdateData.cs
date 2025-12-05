// SocketClient.cs
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class StreamObjectDescriptionsData
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("confidence")]
        public float? Confidence { get; set; }

        [JsonPropertyName("over_threshold")]
        public bool? OverThreshold { get; set; }
    }

    public class StreamUpdateDetectionConfigsData
    {
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("object_descriptions")]
        public StreamObjectDescriptionsData[] ObjectDescriptions { get; set; }
    }

    public class StreamUpdateRegionData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("detection_configs")]
        public StreamUpdateDetectionConfigsData[] DetectionConfigurations { get; set; }
    }

    public class StreamUpdateData
    {
        [JsonPropertyName("stream_url")] 
        public string StreamUrl { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("regions")]
        public StreamUpdateRegionData[] Regions { get; set; }
    }
}
