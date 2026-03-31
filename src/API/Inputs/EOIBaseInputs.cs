using EyesOnItSDK.API.Elements;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIBaseInputs
    {
        [JsonPropertyName("regions")]
        public EOIRegion[] Regions { get; set; }
    }
}
