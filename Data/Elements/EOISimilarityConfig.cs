using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOISimilarityImageConfig
    {
        [JsonPropertyName("seed_id")]
        public string SeedId { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("alert")]
        public bool? Alert { get; set; }

        [JsonPropertyName("threshold")]
        public int? Threshold { get; set; }

        public EOISimilarityImageConfig()
        {

        }
    }

    public class EOISimilarityConfig
    {
        [JsonPropertyName("images")]
        public EOISimilarityImageConfig[] Images { get; set; }

        public EOISimilarityConfig()
        {

        }
    }
}
