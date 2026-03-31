using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOILine
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("vertices")]
        public EOIVertex[] Vertices { get; set; }

        public EOILine()
        {
        }
    }
}
