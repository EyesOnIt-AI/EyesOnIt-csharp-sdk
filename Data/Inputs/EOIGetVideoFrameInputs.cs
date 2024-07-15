using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIGetVideoFrameInputs
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        public EOIGetVideoFrameInputs(string streamUrl) {
            StreamUrl = streamUrl;
        }
    }
}
