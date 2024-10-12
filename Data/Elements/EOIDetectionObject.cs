using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIDetectionObject
    {
        [JsonPropertyName("object_description")]
        public string ObjectDescription { get; set; }

        [JsonPropertyName("confidence")]
        public int Confidence { get; set; }

        [JsonPropertyName("bounds")]
        public EOIBoundingBox Bounds { get; set; }

        public EOIDetectionObject()
        {
            
        }
    }
}
