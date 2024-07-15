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

        [JsonPropertyName("prompts")]
        public EOIObjectDescription[] ObjectDescriptions { get; set; }


        public EOIBaseInputs()
        {
            this.Regions = new EOIRegion[] { };
            this.ObjectDescriptions = new EOIObjectDescription[] { };
        }

        public EOIBaseInputs(EOIObjectDescription[] objectDescriptions, EOIRegion[] regions) 
        {
            this.Regions = regions;
            this.ObjectDescriptions = objectDescriptions;
        }

    }
}
