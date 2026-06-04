using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIDetection
    {
        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        public EOIDetection()
        {
            
        }

        public virtual EOIDetectionObject[] GetDetectedObjects()
        {
            return null;
        }

        public (EOIDetectionObject detectionObject, EOIObjectDescription objectDescription) GetObjectByDescription(string objectDescription)
        {
            EOIDetectionObject[] detectedObjects = GetDetectedObjects();

            if (detectedObjects != null)
            {
                foreach (var detectedObject in detectedObjects)
                {
                    if (detectedObject.ObjectDescriptions == null)
                    {
                        continue;
                    }

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
