using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOISimilarityDetectionObject
    {
        [JsonPropertyName("confidence")]
        public float Confidence { get; set; }
        public EOISimilarityDetectionObject()
        {

        }
    }
}
