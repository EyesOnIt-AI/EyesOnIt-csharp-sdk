using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Elements
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

        public EOIBoundingBox(int left, int top, int width, int height)
        {
            this.Left = left;
            this.Top =  top;
            this.Width = width;
            this.Height = height;
        }
    }
}
