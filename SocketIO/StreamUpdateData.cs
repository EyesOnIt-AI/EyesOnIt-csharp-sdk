// SocketClient.cs
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class StreamObjectDescriptionsData
    {
        [JsonProperty("text")]
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonProperty("confidence")]
        [JsonPropertyName("confidence")]
        public float? Confidence { get; set; }

        [JsonProperty("over_threshold")]
        [JsonPropertyName("over_threshold")]
        public bool? OverThreshold { get; set; }
    }

    public class StreamUpdateDetectionConfigsData
    {
        [JsonProperty("class_name")]
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonProperty("object_descriptions")]
        [JsonPropertyName("object_descriptions")]
        public StreamObjectDescriptionsData[] ObjectDescriptions { get; set; }
    }

    public class StreamUpdateRegionData
    {
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonProperty("detection_configs")]
        [JsonPropertyName("detection_configs")]
        public StreamUpdateDetectionConfigsData[] DetectionConfigurations { get; set; }
    }

    public class StreamUpdateData
    {
        [JsonProperty("stream_url")]
        [JsonPropertyName("stream_url")] 
        public string StreamUrl { get; set; }

        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonProperty("regions")]
        [JsonPropertyName("regions")]
        public StreamUpdateRegionData[] Regions { get; set; }
    }
}
