using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EyesOnItSDK.Data.Elements;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIBaseInputs
    {
        [JsonPropertyName("regions")]
        public EOIRegion[] Regions { get; set; }
    }
}
