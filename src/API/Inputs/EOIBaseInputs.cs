using EyesOnItSDK.Data.Elements;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIBaseInputs
    {
        [JsonPropertyName("regions")]
        public EOIRegion[] Regions { get; set; }
    }
}
