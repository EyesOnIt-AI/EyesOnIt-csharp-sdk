using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOILine
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("vertices")]
        public EOIVertex[] Vertices { get; set; }

        public EOILine()
        {
        }
    }
}
