using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIBoundingBox
    {
        [JsonPropertyName("top")]
        public int Top { get; set; }

        [JsonPropertyName("left")]
        public int Left { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        public int Right => Left + Width;

        public int Bottom => Top + Height;

        public EOIBoundingBox(int left, int top, int width, int height)
        {
            this.Left = left;
            this.Top =  top;
            this.Width = width;
            this.Height = height;
        }
    }
}
