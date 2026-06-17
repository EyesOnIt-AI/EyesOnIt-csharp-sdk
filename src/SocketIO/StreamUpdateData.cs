// SocketClient.cs
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class SocketEnvelopeData
    {
        [JsonPropertyName("schema_version")]
        public int SchemaVersion { get; set; }

        [JsonPropertyName("server_instance_id")]
        public string ServerInstanceId { get; set; }

        [JsonPropertyName("room")]
        public string Room { get; set; }

        [JsonPropertyName("message_type")]
        public string MessageType { get; set; }

        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }

        [JsonPropertyName("sent_at")]
        public string SentAt { get; set; }
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
    }

    public class StreamUpdateChangeData
    {
        [JsonPropertyName("op")]
        public string Op { get; set; }

        [JsonPropertyName("stream")]
        public StreamUpdateData Stream { get; set; }

        [JsonPropertyName("stream_id")]
        public string StreamId { get; set; }

        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }
    }

    public class StreamUpdateEnvelopeData : SocketEnvelopeData
    {
        [JsonPropertyName("streams")]
        public StreamUpdateData[] Streams { get; set; }

        [JsonPropertyName("changes")]
        public StreamUpdateChangeData[] Changes { get; set; }
    }
}
