using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIVertex
    {
        [JsonPropertyName("x")]
        [JsonConverter(typeof(EOIFloatToIntConverter))]
        public int X { get; set; }

        [JsonPropertyName("y")]
        [JsonConverter(typeof(EOIFloatToIntConverter))]
        public int Y { get; set; }

        public EOIVertex(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
