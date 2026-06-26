using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIDetectionObject : EOIBase64Image
    {
        [JsonPropertyName("object_descriptions")]
        public EOIObjectDescription[] ObjectDescriptions { get; set; }

        [JsonPropertyName("detection_types")]
        public string[] DetectionTypes { get; set; }

        [JsonPropertyName("class_confidence")]
        public float? ClassConfidence { get; set; }

        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("bounds")]
        public EOIBoundingBox Bounds { get; set; }

        [JsonPropertyName("face")]
        public EOIFaceDetectionObject Face { get; set; }

        [JsonPropertyName("similarity")]
        public EOISimilarityDetectionObject Similarity { get; set; }

        public EOIDetectionObject()
        {
            
        }
        public double? GetConfidenceForDescription(string description)
        {
            if (ObjectDescriptions == null)
            {
                return null;
            }

            foreach (var objectDescription in ObjectDescriptions)
            {
                if (objectDescription.Text == description)
                {
                    return objectDescription.Confidence != 0 ? objectDescription.Confidence : null;
                }
            }

            return null;
        }
        public (string Description, double Confidence)? GetMaxConfidenceDescription()
        {
            double maxConfidence = -1;
            string maxConfidenceDescription = null;

            if (ObjectDescriptions != null)
            {
                foreach (var objectDescription in ObjectDescriptions)
                {
                    if (objectDescription.Confidence != null && objectDescription.Confidence > maxConfidence)
                    {
                        maxConfidence = objectDescription.Confidence.Value;
                        maxConfidenceDescription = objectDescription.Text;
                    }
                }
            }

            if (maxConfidenceDescription != null)
            {
                return (maxConfidenceDescription, maxConfidence);
            }
            else
            {
                return null;
            }
        }
    }
}
