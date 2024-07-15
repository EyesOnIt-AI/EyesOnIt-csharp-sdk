using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EyesOnItSDK.Data.Elements;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIProcessImageInputs : EOIBaseInputs
    {
        [JsonPropertyName("file")]
        public string Base64Image { get; set; }

        [JsonPropertyName("object_size")]
        public int ObjectSize { get; set; }

        public EOIProcessImageInputs(string base64Image, EOIRegion[] regions, int objectSize, EOIObjectDescription[] objectDescriptions) 
            : base(objectDescriptions, regions)
        {
            Base64Image = base64Image;
            ObjectSize = objectSize;
        }
    }
}
