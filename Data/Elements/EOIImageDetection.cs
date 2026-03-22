using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIImageDetection : EOIDetection
    {
        [JsonPropertyName("objects")]
        public EOIDetectionObject[] Objects { get; set; }


        public EOIImageDetection() : base()
        {
            
        }

        public EOIDetectionObject[] GetDetectedObjects() 
        { 
            return Objects;
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
