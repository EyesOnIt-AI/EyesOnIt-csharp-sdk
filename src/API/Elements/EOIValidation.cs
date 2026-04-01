using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIValidation
    {
        [JsonPropertyName("triggers")]
        public EOIValidationTrigger[] Triggers { get; set; }
    }
}
