using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
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
    }
}
