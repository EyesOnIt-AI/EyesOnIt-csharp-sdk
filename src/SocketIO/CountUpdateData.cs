using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class CountUpdateData
    {
        [JsonPropertyName("stream_name")]
        public string StreamName { get; set; }

        [JsonPropertyName("region_name")]
        public string RegionName { get; set; }

        [JsonPropertyName("object_type")]
        public string ObjectType { get; set; }

        [JsonPropertyName("frame_number")]
        public int? FrameNumber { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    public class CountUpdateDataWrapper
    {
        [JsonPropertyName("count")]
        public CountUpdateData Count { get; set; }
    }
}
