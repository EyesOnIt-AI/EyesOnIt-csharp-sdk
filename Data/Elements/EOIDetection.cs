using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIDetection
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("time")]
        [JsonConverter(typeof(DateTimeConverterUsingDateTimeParse))]
        public DateTime Time { get; set; }

        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("condition")]
        public EOIDetectionCondition Condition { get; set; }

        [JsonPropertyName("objects")]
        public EOIDetectionObject[] Objects { get; set; }

        public EOIDetection()
        {
            
        }

        public EOIDetectionObject[] GetDetectedObjects() 
        { 
            if (Condition != null)
            {
                return Condition.Objects;
            }
            else
            {
                return Objects;
            }
        }

        public EOIDetectionObject GetObjectByDescription(string objectDescription)
        {
            EOIDetectionObject[] objects = GetDetectedObjects();

            if (objects != null)
            {
                foreach (var obj in objects)
                {
                    if (obj.ObjectDescription == objectDescription)
                    {
                        return obj;
                    }
                }
            }

            return null;
        }
    }
}
