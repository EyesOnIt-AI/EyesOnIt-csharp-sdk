// SocketClient.cs
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class PerformanceGpuUpdateData
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("util_pct")]
        public float? UtilPct { get; set; }

        [JsonPropertyName("vram_pct")]
        public float? VramPct { get; set; }
    }

    public class PerformanceSystemUpdateData
    {
        [JsonPropertyName("cpu_pct")]
        public float? CpuPct { get; set; }

        [JsonPropertyName("ram_pct")]
        public float? RamPct { get; set; }
    }

    public class PerformanceUpdateData
    {
        [JsonPropertyName("schema_version")]
        public int SchemaVersion { get; set; }

        [JsonPropertyName("server_instance_id")]
        public string ServerInstanceId { get; set; }

        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }

        [JsonPropertyName("sent_at")]
        public string SentAt { get; set; }

        [JsonPropertyName("gpus")]
        public PerformanceGpuUpdateData[] Gpus { get; set; }

        [JsonPropertyName("system")]
        public PerformanceSystemUpdateData System { get; set; }
    }
}
