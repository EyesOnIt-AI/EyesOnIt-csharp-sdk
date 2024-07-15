using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIRemoveStreamInputs
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        public EOIRemoveStreamInputs(string streamUrl)
        {
            StreamUrl = streamUrl;
        }
    }
}
