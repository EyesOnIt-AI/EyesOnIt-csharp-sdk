// SocketClient.cs
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class PerformanceStreamData
    {
        [JsonProperty("stream_url")]
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        [JsonProperty("cpu_percent")]
        [JsonPropertyName("cpu_percent")]
        public float CPUPercent { get; set; }

        [JsonProperty("gpu_percent")]
        [JsonPropertyName("gpu_percent")]
        public float GPUPercent { get; set; }
    }

    public class PerformanceTotalsData
    {
        [JsonProperty("stream_url")]
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        [JsonProperty("cpu_percent")]
        [JsonPropertyName("cpu_percent")]
        public float CPUPercent { get; set; }

        [JsonProperty("gpu_percent")]
        [JsonPropertyName("gpu_percent")]
        public float GPUPercent { get; set; }
    }

    public class PerformanceUpdateData
    {
        [JsonProperty("streams")]
        [JsonPropertyName("streams")] 
        public PerformanceStreamData[] Streams { get; set; }

        [JsonProperty("totals")]
        [JsonPropertyName("totals")]
        public PerformanceTotalsData Totals { get; set; }
    }

    public class PerformanceUpdateDataWrapper
    {
        [JsonProperty("performance")]
        [JsonPropertyName("performance")]
        public PerformanceUpdateData Performance { get; set; }
    }
}
