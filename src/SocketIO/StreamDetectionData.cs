// SocketClient.cs
using EyesOnItSDK.API.Elements;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class StreamDetectionConditionData
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("count")]
        public int? Count{ get; set; }

        [JsonPropertyName("line_name")]
        public string LineName { get; set; }

        [JsonPropertyName("alert_direction")]
        public string AlertDirection { get; set; }

        [JsonPropertyName("objects")]
        public EOIDetectionObject[] Objects { get; set; }
    }

    public class StreamDetectionData : EOIBase64Image
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        [JsonPropertyName("stream_name")]
        public string StreamName { get; set; }

        [JsonPropertyName("event")]
        public string Event { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("frame_num")]
        public int? FrameNum { get; set; }

        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("object_description")]
        public string ObjectDescription { get; set; }

        [JsonPropertyName("condition")]
        public StreamDetectionConditionData Condition { get; set; }

        [JsonPropertyName("total_count")]
        public int? TotalCount { get; set; }

        [JsonPropertyName("result_id")]
        public string ResultId { get; set; }

        [JsonPropertyName("alert_stream_id")]
        public string AlertStreamId { get; set; }

        [JsonPropertyName("alert_id")]
        public string AlertId { get; set; }

        [JsonPropertyName("alert_rtsp_url")]
        public string AlertRtspUrl { get; set; }
    }

    public class StreamDetectionsData : EOIBase64Image
    {
        [JsonPropertyName("detections")] 
        public StreamDetectionData[] Detections { get; set; }
    }
}
