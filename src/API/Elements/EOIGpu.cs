using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIGpu
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("util_pct")]
        public double UtilPct { get; set; }

        [JsonPropertyName("mem_pct")]
        public double MemPct { get; set; }

        [JsonPropertyName("mem_free_mb")]
        public int MemFreeMb { get; set; }

        [JsonPropertyName("dec_pct")]
        public double DecPct { get; set; }

        [JsonPropertyName("enc_pct")]
        public double EncPct { get; set; }

        [JsonPropertyName("temp_f")]
        public double TempF { get; set; }

        [JsonPropertyName("load_score_pct")]
        public double LoadScorePct { get; set; }
    }
}
