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
    }
}
