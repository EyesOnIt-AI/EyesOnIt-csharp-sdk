using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIDetection
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        [JsonPropertyName("stream_name")]
        public string StreamName { get; set; }

        [JsonPropertyName("event")]
        public string Event { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("time")]
        [JsonConverter(typeof(DateTimeConverterUsingDateTimeParse))]
        public DateTime Time { get; set; }

        [JsonPropertyName("frame_num")]
        public int FrameNumber { get; set; }

        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("object_description")]
        public string ObjectDescription { get; set; }

        [JsonPropertyName("condition")]
        public EOIDetectionCondition Condition { get; set; }

        [JsonPropertyName("result_id")]
        public string ResultId { get; set; }

        public EOIDetection()
        {
            
        }

        public EOIDetectionObject[] GetDetectedObjects() 
        { 
            return Condition.Objects;
        }

        public (EOIDetectionObject detectionObject, EOIObjectDescription objectDescription) GetObjectByDescription(string objectDescription)
        {
            EOIDetectionObject[] detectedObjects = GetDetectedObjects();

            if (detectedObjects != null)
            {
                foreach (var detectedObject in detectedObjects)
                {
                    foreach (var detectedObjectDescription in detectedObject.ObjectDescriptions)
                    {
                        if (detectedObjectDescription.Text == objectDescription)
                        {
                            return (detectedObject, detectedObjectDescription);
                        }
                    }
                }
            }

            return (null, null);
        }
    }
}
