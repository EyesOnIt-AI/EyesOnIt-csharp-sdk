using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOISystemHealth
    {
        [JsonPropertyName("cpu_pct")]
        public double CpuPct { get; set; }

        [JsonPropertyName("ram_pct")]
        public double RamPct { get; set; }

        [JsonPropertyName("ram_free_mb")]
        public int RamFreeMb { get; set; }
    }
}
