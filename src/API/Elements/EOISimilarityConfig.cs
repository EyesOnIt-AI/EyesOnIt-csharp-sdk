using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOISimilarityImage
    {
        [JsonPropertyName("seed_id")]
        public string SeedId { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("alert")]
        public bool? Alert { get; set; }

        [JsonPropertyName("threshold")]
        public int? Threshold { get; set; }

        public EOISimilarityImage()
        {

        }
    }

    public class EOISimilarityConfig
    {
        [JsonPropertyName("images")]
        public EOISimilarityImage[] Images { get; set; }

        public EOISimilarityConfig()
        {

        }
    }
}
