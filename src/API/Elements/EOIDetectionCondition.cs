using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public enum AlertDirection
    {
        Positive = 1,
        Negative = 2,
        None = 3
    }

    public class EOIDetectionCondition
    {
        [JsonPropertyName("type")]
        public string Type{ get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("line_name")]
        public string LineName { get; set; }

        [JsonPropertyName("alert_direction")]
        public string AlertDirectionStr { get; set; }

        [JsonPropertyName("objects")]
        public EOIDetectionObject[] Objects { get; set; }


        [JsonIgnore] // Ignore the AlertDirection property in serialization
        public AlertDirection AlertDirection
        {
            get
            {
                if (AlertDirectionStr == null || AlertDirectionStr.Length == 0)
                {
                    return AlertDirection.None;
                }
                else
                {
                    // Translate _AlertDirection to AlertDirection when getting the property
                    return AlertDirectionStr.ToLower() == "positive" ? AlertDirection.Positive : AlertDirection.Negative;
                }
            }
            set
            {
                // Set _AlertDirection based on the enum value
                AlertDirectionStr = value == AlertDirection.Positive ? "positive" : "negative";
            }
        }

        public EOIDetectionCondition()
        { 
        }

        public (string, double)? GetMaxConfidenceObject()
        {
            double maxConfidence = -1;
            string maxConfidenceDescription = null;

            if (Objects != null)
            {
                foreach (var obj in Objects)
                {
                    double confidence = 0;
                    string description = null;

                    switch (obj.DetectionType)
                    {
                        case "class_name":
                            confidence = obj.ClassConfidence;
                            description = obj.ClassName ?? "";
                            break;
                        case "natural_language":
                            var response = obj.GetMaxConfidenceDescription();

                            if (response != null)
                            {
                                (description, confidence) = response.Value;
                            }
                            break;
                        case "face_recognition":
                            confidence = obj.Face?.Confidence ?? 0;
                            description = obj.Face?.PersonDisplayName;
                            break;
                        case "similarity":
                            confidence = obj.Similarity?.Confidence ?? 0;
                            description = "Similar person";
                            break;
                    }

                    if (confidence > maxConfidence)
                    {
                        maxConfidence = confidence;
                        maxConfidenceDescription = description;
                    }
                }
            }

            if (!string.IsNullOrEmpty(maxConfidenceDescription) && maxConfidence > 0)
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
