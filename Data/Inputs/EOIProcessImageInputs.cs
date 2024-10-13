using EyesOnItSDK.Data.Elements;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIProcessImageInputs : EOIBaseInputs
    {
        [JsonPropertyName("file")]
        public string Base64Image { get; set; }

        public EOIProcessImageInputs(string base64Image, EOIRegion[] regions) 
            : base()
        {
            Regions = regions;
            Base64Image = base64Image;

        }
    }
}
