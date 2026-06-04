using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIImageDetection : EOIDetection
    {
        [JsonPropertyName("objects")]
        public EOIDetectionObject[] Objects { get; set; }


        public EOIImageDetection() : base()
        {
            
        }

        public override EOIDetectionObject[] GetDetectedObjects() 
        { 
            return Objects;
        }
    }
}
