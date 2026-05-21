// SocketClient.cs
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class StreamObjectDescriptionsData
    {
        [JsonPropertyName("display_text")]
        public string DisplayText { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("threshold")]
        public int? Threshold { get; set; }

        [JsonPropertyName("alert")]
        public bool? Alert { get; set; }

        [JsonPropertyName("confidence")]
        public float? Confidence { get; set; }

        [JsonPropertyName("over_threshold")]
        public bool? OverThreshold { get; set; }

        [JsonPropertyName("background_prompt")]
        public bool? BackgroundPrompt { get; set; }
    }

    public class StreamUpdateObjectData
    {
        [JsonPropertyName("class_confidence")]
        public float? ClassConfidence { get; set; }

        [JsonPropertyName("object_descriptions")]
        public StreamObjectDescriptionsData[] ObjectDescriptions { get; set; }
    }

    public class StreamUpdateDetectionConfigsData
    {
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("object_descriptions")]
        public StreamObjectDescriptionsData[] ObjectDescriptions { get; set; }

        [JsonPropertyName("objects")]
        public StreamUpdateObjectData[] Objects { get; set; }
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

        [JsonPropertyName("stream_id")]
        public string StreamId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("regions")]
        public StreamUpdateRegionData[] Regions { get; set; }
    }
}
