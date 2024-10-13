using System.Text.Json.Serialization;

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
