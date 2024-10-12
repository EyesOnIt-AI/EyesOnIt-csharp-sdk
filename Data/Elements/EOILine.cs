using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
