using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIVertex
    {
        [JsonPropertyName("x")]
        public float X { get; set; }

        [JsonPropertyName("y")]
        public float Y { get; set; }

        public EOIVertex()
        {
        }

        public EOIVertex(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
