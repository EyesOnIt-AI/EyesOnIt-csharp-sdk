using EyesOnItSDK.Data.Elements;
using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIProcessImageInputs : EOIBaseInputs
    {
        [JsonPropertyName("file")]
        public string Base64Image { get; set; }

        [JsonPropertyName("return_image")]
        public bool ReturnImage { get; set; }

        [JsonPropertyName("effects")]
        public EOIEffects Effects { get; set; }

        public EOIProcessImageInputs(string base64Image, EOIRegion[] regions) 
            : base()
        {
            Regions = regions;
            Base64Image = base64Image;
            ReturnImage = false;
        }
    }
}
