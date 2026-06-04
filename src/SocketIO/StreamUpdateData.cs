// SocketClient.cs
using EyesOnItSDK.API.Elements;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class StreamUpdateObjectData
    {
        [JsonPropertyName("class_confidence")]
        public float? ClassConfidence { get; set; }

        [JsonPropertyName("object_descriptions")]
        public EOIObjectDescription[] ObjectDescriptions { get; set; }
    }

    public class StreamUpdateDetectionConfigsData
    {
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("object_descriptions")]
        public EOIObjectDescription[] ObjectDescriptions { get; set; }

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
