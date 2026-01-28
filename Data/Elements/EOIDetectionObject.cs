using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIDetectionObject
    {
        [JsonPropertyName("object_descriptions")]
        public EOIObjectDescription[] ObjectDescriptions { get; set; }

        [JsonPropertyName("bounds")]
        public EOIBoundingBox Bounds { get; set; }

        public EOIDetectionObject()
        {
            
        }
    }
}
