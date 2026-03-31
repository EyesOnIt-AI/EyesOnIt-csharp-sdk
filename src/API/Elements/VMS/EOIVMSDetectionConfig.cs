using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements.VMS
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
