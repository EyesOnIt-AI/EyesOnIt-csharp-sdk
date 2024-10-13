using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIVertex
    {
        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }

        public EOIVertex(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
