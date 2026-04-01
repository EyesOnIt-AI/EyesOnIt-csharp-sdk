using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements.VMS
{
    public class EOIVMSDetectionConfig
    {
        // Genetec Properties
        [JsonPropertyName("genetec")]
        public EOIGenetecDetectionConfig Genetec { get; set; }


        public EOIVMSDetectionConfig()
        {
        }
    }
}
