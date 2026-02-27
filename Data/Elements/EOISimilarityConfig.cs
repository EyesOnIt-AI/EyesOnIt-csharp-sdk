using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOISimilarityConfig
    {
        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("match_threshold")]
        public int? MatchThreshold { get; set; }

        public EOISimilarityConfig()
        {

        }
    }
}
