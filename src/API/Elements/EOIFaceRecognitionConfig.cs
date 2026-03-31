using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIFaceRecognitionConfig
    {
        [JsonPropertyName("match_type")]
        public string MatchType { get; set; }

        [JsonPropertyName("match_threshold")]
        public int? MatchThreshold { get; set; }

        [JsonPropertyName("person")]
        public string Person { get; set; }

        [JsonPropertyName("group")]
        public string Group { get; set; }

        public EOIFaceRecognitionConfig()
        {

        }
    }
}
