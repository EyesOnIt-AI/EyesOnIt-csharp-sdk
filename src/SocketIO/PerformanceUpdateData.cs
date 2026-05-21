// SocketClient.cs
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class PerformanceStreamData
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        [JsonPropertyName("cpu_percent")]
        public float CPUPercent { get; set; }

        [JsonPropertyName("gpu_percent")]
        public float GPUPercent { get; set; }
    }

    public class PerformanceTotalsData
    {
        [JsonPropertyName("cpu_percent")]
        public float CPUPercent { get; set; }

        [JsonPropertyName("gpu_percent")]
        public float GPUPercent { get; set; }
    }

    public class PerformanceUpdateData
    {
        [JsonPropertyName("streams")] 
        public PerformanceStreamData[] Streams { get; set; }

        [JsonPropertyName("totals")]
        public PerformanceTotalsData Totals { get; set; }
    }

    public class PerformanceUpdateDataWrapper
    {
        [JsonPropertyName("performance")]
        public PerformanceUpdateData Performance { get; set; }
    }
}
