using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
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
